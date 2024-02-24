using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeSample_Currency.Currency
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

        private System.Random _random = new System.Random();

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
        /// Packs a wallet containing two types of currencies from a wallet containing three types of currencies based on their worth.
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
            Dictionary<CurrencyType, int> wallet,
            Dictionary<CurrencyType, int> currenciesWorth)
        {
            int copperWorth = currenciesWorth[CurrencyType.Copper];
            int silverWorth = currenciesWorth[CurrencyType.Silver];
            int goldWorth = currenciesWorth[CurrencyType.Gold];

            Dictionary<CurrencyType, int> result = new();
            foreach (var (key, value) in wallet)
                result.Add(key, value);

            int totalCopperWorth = copperWorth * result[CurrencyType.Copper];
            int totalSilverWorth = silverWorth * result[CurrencyType.Silver];
            int totalGoldWorth = goldWorth * result[CurrencyType.Gold];

            if (totalCopperWorth % goldWorth % silverWorth == 0)
            {
                result[CurrencyType.Gold] += totalCopperWorth / goldWorth;
                result[CurrencyType.Silver] += totalCopperWorth % goldWorth / silverWorth;
                result[CurrencyType.Copper] = 0;
            }
            else if (totalCopperWorth % goldWorth == 0)
            {
                result[CurrencyType.Gold] += totalCopperWorth / goldWorth;
                result[CurrencyType.Copper] = 0;
            }
            else if (totalCopperWorth % silverWorth == 0)
            {
                result[CurrencyType.Silver] += totalCopperWorth / silverWorth;
                result[CurrencyType.Copper] = 0;
            }
            else if (totalSilverWorth % goldWorth == 0)
            {
                result[CurrencyType.Gold] += totalSilverWorth / goldWorth;
                result[CurrencyType.Silver] = 0;
            }
            else
            {
                if (totalGoldWorth % silverWorth == 0)
                {
                    result[CurrencyType.Silver] += totalGoldWorth / silverWorth;
                    result[CurrencyType.Gold] = 0;
                }
                else if (totalGoldWorth % silverWorth % copperWorth == 0)
                {
                    result[CurrencyType.Silver] += totalGoldWorth / silverWorth;
                    result[CurrencyType.Copper] += totalGoldWorth % silverWorth / copperWorth;
                    result[CurrencyType.Gold] = 0;
                }
                else if (totalSilverWorth % copperWorth == 0)
                {
                    result[CurrencyType.Copper] += totalSilverWorth / copperWorth;
                    result[CurrencyType.Silver] = 0;
                }
                else if (totalGoldWorth % copperWorth == 0)
                {
                    result[CurrencyType.Copper] += totalGoldWorth / copperWorth;
                    result[CurrencyType.Gold] = 0;
                }
            }
            return result;
        }
    }
}
