using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeSample_Currencies.Currency
{
    [CreateAssetMenu(fileName = "CurrencyHelper", menuName = "Scriptable Objects/CurrencyHelper")]
    public class CurrencyHelper : SerializedScriptableObject
    {
        #region Singleton
        private static string s_assetName => nameof(CurrencyHelper);
        private static string s_loadPath => $"Helpers/{s_assetName}";
        private static string s_savePath => $"Assets/Resources/{s_loadPath}.asset";

        private static CurrencyHelper s_instance;

        public static CurrencyHelper Instance
        {
            get
            {
                if (s_instance != null) return s_instance;
                s_instance = Resources.Load<CurrencyHelper>(s_loadPath);
                if (s_instance != null) return s_instance;
                s_instance = CreateInstance<CurrencyHelper>();
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.CreateAsset(s_instance, s_savePath);
#endif
                return s_instance;
            }
        }
        #endregion

        [field: SerializeField] public int CurrencyRemainderIgnoreThreshold { get; private set; } = 2;
        [field: SerializeField] public Dictionary<CurrencyType, CurrencyInfo> CurrencyInfos { get; private set; }

        public Dictionary<CurrencyType, int> GetEmptyWallet()
        {
            Dictionary<CurrencyType, int> result = new();

            foreach (var (currencyType, _) in CurrencyInfos)
                result.Add(currencyType, 0);

            return result;
        }

        public Dictionary<CurrencyType, int> GetAllCurrenciesDefaultWorth()
        {
            Dictionary<CurrencyType, int> result = new();

            foreach (var (currencyType, currencyInfo) in CurrencyInfos)
                result.Add(currencyType, currencyInfo.Worth);

            return result;
        }

        public Dictionary<CurrencyType, int> GetMoneyInRandomCurrency(
            Dictionary<CurrencyType, int> currenciesWorth, int moneyToGet)
        {
            var wallet = GetEmptyWallet();

            List<CurrencyType> possibleCurrencies = new();
            foreach (var (currencyType, worth) in currenciesWorth)
            {
                if (worth <= moneyToGet)
                    possibleCurrencies.Add(currencyType);
            }

            if (possibleCurrencies.Count == 0)
                return wallet;

            int randomIndex = Random.Range(0, possibleCurrencies.Count);
            CurrencyType selectedCurrency = possibleCurrencies[randomIndex];

            FillWalletWithCurrency(wallet, currenciesWorth, selectedCurrency, moneyToGet);

            return wallet;
        }

        void FillWalletWithCurrency(Dictionary<CurrencyType, int> wallet,
            in Dictionary<CurrencyType, int> currenciesWorth,
            CurrencyType currencyType,
            int value)
        {
            int selectedCurrencyQuantity = value / currenciesWorth[currencyType];
            wallet[currencyType] += selectedCurrencyQuantity;

            int generatedValue = selectedCurrencyQuantity * currenciesWorth[currencyType];

            if (generatedValue < value)
            {
                int remainingValue = value - generatedValue;
                if (remainingValue > CurrencyRemainderIgnoreThreshold)
                {
                    wallet[currencyType]++;
                }
            }
        }

        public Dictionary<CurrencyType, int> GetMoneyInRandomCurrencies(
            Dictionary<CurrencyType, int> currenciesWorth, int targetWorth, bool maxTwo = false)
        {
            var wallet = GetEmptyWallet();

            List<CurrencyType> possibleCurrencies = new();
            foreach (var (currencyType, worth) in currenciesWorth
                .Where((currencyType, worth) => worth <= targetWorth))
                possibleCurrencies.Add(currencyType);

            int generatedWorth = 0;
            while (generatedWorth < targetWorth)
            {
                int randomIndex = Random.Range(0, possibleCurrencies.Count);
                CurrencyType selectedCurrency = possibleCurrencies[randomIndex];

                generatedWorth += currenciesWorth[selectedCurrency];

                wallet[selectedCurrency]++;

                List<CurrencyType> newPossibleCurrencies = new();
                foreach (var currencyType in possibleCurrencies)
                {
                    if (generatedWorth + currenciesWorth[currencyType] <= targetWorth)
                        newPossibleCurrencies.Add(currencyType);
                }
                possibleCurrencies = newPossibleCurrencies;
                if (possibleCurrencies.Count == 0)
                    break;
            }

            if (maxTwo && wallet.Values.All(quantity => quantity > 0))
            {
                wallet = PackTreeCurrenciesIntoTwo(wallet, currenciesWorth);
            }

            return wallet;
        }

        /// <summary>
        /// Packs a wallet containing one or two types of currencies from another wallet containing three types of currencies based on their worth.
        /// </summary>
        /// <param name="wallet">The wallet containing the initial amounts of each type of currency.</param>
        /// <param name="currenciesWorth">The relative worth of each currency type.</param>
        /// <returns>A dictionary containing the updated amounts of Copper, Silver, and Gold after packing.</returns>
        /// <remarks>
        /// This method calculates the total worth of each currency type in the wallet based on the worth of the other two currencies.
        /// It then attempts to convert the currencies to two types by finding the largest currency type that can be evenly divided by the worth of the other two.
        /// If a currency type cannot be converted, it carries over unchanged into the new wallet.
        /// </remarks>
        public Dictionary<CurrencyType, int> PackTreeCurrenciesIntoTwo(
            in Dictionary<CurrencyType, int> wallet,
            in Dictionary<CurrencyType, int> currenciesWorth)
        {
            int copperWorth = currenciesWorth[CurrencyType.Copper];
            int silverWorth = currenciesWorth[CurrencyType.Silver];
            int goldWorth = currenciesWorth[CurrencyType.Gold];

            Dictionary<CurrencyType, int> result = new()
            {
                { CurrencyType.Copper, wallet[CurrencyType.Copper] },
                { CurrencyType.Silver, wallet[CurrencyType.Silver] },
                { CurrencyType.Gold, wallet[CurrencyType.Gold] }
            };

            int totalCopperWorth = copperWorth * result[CurrencyType.Copper];
            int totalSilverWorth = silverWorth * result[CurrencyType.Silver];
            int totalGoldWorth = goldWorth * result[CurrencyType.Gold];

            var possibleConversionPaths = GetPossibleConversions(
                totalCopperWorth, totalSilverWorth, totalGoldWorth,
                copperWorth, silverWorth, goldWorth);
            ConversionType conversionPath = possibleConversionPaths
                [Random.Range(0, possibleConversionPaths.Count)];

            Convert(result, conversionPath,
                totalCopperWorth, totalSilverWorth, totalGoldWorth,
                copperWorth, silverWorth, goldWorth);

            return result;
        }

        List<ConversionType> GetPossibleConversions(
            int copperTotalWorth, int silverTotalWorth, int goldTotalWorth,
            int copperWorth, int silverWorth, int goldWorth)
        {
            List<ConversionType> conversions = new();

            if (copperTotalWorth % silverWorth == 0)
                conversions.Add(ConversionType.CopperToSilver);
            else if (copperTotalWorth % silverWorth % goldWorth == 0)
                conversions.Add(ConversionType.CopperToSilverGold);

            if (copperTotalWorth % goldWorth == 0)
                conversions.Add(ConversionType.CopperToGold);
            else if (copperTotalWorth % goldWorth % silverWorth == 0)
                conversions.Add(ConversionType.CopperToGoldSilver);

            if (silverTotalWorth % copperWorth == 0)
                conversions.Add(ConversionType.SilverToCopper);
            else if (silverTotalWorth % copperWorth % goldWorth == 0)
                conversions.Add(ConversionType.SilverToCopperGold);

            if (silverTotalWorth % goldWorth == 0)
                conversions.Add(ConversionType.SilverToGold);
            else if (silverTotalWorth % goldWorth % copperWorth == 0)
                conversions.Add(ConversionType.SilverToGoldCopper);

            if (goldTotalWorth % copperWorth == 0)
                conversions.Add(ConversionType.GoldToCopper);
            else if (goldTotalWorth % copperWorth % silverWorth == 0)
                conversions.Add(ConversionType.GoldToCopperSilver);

            if (goldTotalWorth % silverWorth == 0)
                conversions.Add(ConversionType.GoldToSilver);
            else if (goldTotalWorth % silverWorth % copperWorth == 0)
                conversions.Add(ConversionType.GoldToSilverCopper);

            return conversions;
        }

        void Convert(Dictionary<CurrencyType, int> result,
            ConversionType conversionPath,
            int totalCopperWorth, int totalSilverWorth, int totalGoldWorth,
            int copperWorth, int silverWorth, int goldWorth)
        {
            switch (conversionPath)
            {
                case ConversionType.CopperToSilverGold:
                    result[CurrencyType.Copper] = 0;
                    result[CurrencyType.Silver] += totalCopperWorth / silverWorth;
                    result[CurrencyType.Gold] += totalCopperWorth % silverWorth / goldWorth;
                    break;

                case ConversionType.CopperToGoldSilver:
                    result[CurrencyType.Copper] = 0;
                    result[CurrencyType.Gold] += totalCopperWorth / goldWorth;
                    result[CurrencyType.Silver] += totalCopperWorth % goldWorth / silverWorth;
                    break;

                case ConversionType.CopperToSilver:
                    result[CurrencyType.Copper] = 0;
                    result[CurrencyType.Silver] += totalCopperWorth / silverWorth;
                    break;

                case ConversionType.CopperToGold:
                    result[CurrencyType.Copper] = 0;
                    result[CurrencyType.Gold] += totalCopperWorth / goldWorth;
                    break;

                case ConversionType.SilverToCopperGold:
                    result[CurrencyType.Silver] = 0;
                    result[CurrencyType.Copper] += totalSilverWorth / copperWorth;
                    result[CurrencyType.Gold] += totalSilverWorth % copperWorth / goldWorth;
                    break;

                case ConversionType.SilverToGoldCopper:
                    result[CurrencyType.Silver] = 0;
                    result[CurrencyType.Gold] += totalSilverWorth / goldWorth;
                    result[CurrencyType.Copper] += totalSilverWorth % goldWorth / copperWorth;
                    break;

                case ConversionType.SilverToCopper:
                    result[CurrencyType.Silver] = 0;
                    result[CurrencyType.Copper] += totalSilverWorth / copperWorth;
                    break;

                case ConversionType.SilverToGold:
                    result[CurrencyType.Silver] = 0;
                    result[CurrencyType.Gold] += totalSilverWorth / goldWorth;
                    break;

                case ConversionType.GoldToCopperSilver:
                    result[CurrencyType.Gold] = 0;
                    result[CurrencyType.Copper] += totalGoldWorth / copperWorth;
                    result[CurrencyType.Silver] += totalGoldWorth % copperWorth / silverWorth;
                    break;

                case ConversionType.GoldToSilverCopper:
                    result[CurrencyType.Gold] = 0;
                    result[CurrencyType.Silver] += totalGoldWorth / silverWorth;
                    result[CurrencyType.Copper] += totalGoldWorth % silverWorth / copperWorth;
                    break;

                case ConversionType.GoldToCopper:
                    result[CurrencyType.Gold] = 0;
                    result[CurrencyType.Copper] += totalGoldWorth / copperWorth;
                    break;

                case ConversionType.GoldToSilver:
                    result[CurrencyType.Gold] = 0;
                    result[CurrencyType.Silver] += totalGoldWorth / silverWorth;
                    break;
            }
        }

        enum ConversionType : byte
        {
            None = 0,

            CopperToSilverGold = 1,
            CopperToGoldSilver = 2,
            CopperToSilver = 3,
            CopperToGold = 4,

            SilverToCopperGold = 5,
            SilverToGoldCopper = 6,
            SilverToCopper = 7,
            SilverToGold = 8,

            GoldToCopperSilver = 10,
            GoldToSilverCopper = 11,
            GoldToCopper = 12,
            GoldToSilver = 13,
        }
    }
}
