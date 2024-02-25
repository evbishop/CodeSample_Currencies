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

        public Dictionary<CurrencyType, int> PackTreeCurrenciesIntoTwo(
            in Dictionary<CurrencyType, int> wallet,
            in Dictionary<CurrencyType, int> currenciesWorth)
        {
            int copperWorth = currenciesWorth[CurrencyType.Copper];
            int silverWorth = currenciesWorth[CurrencyType.Silver];
            int goldWorth = currenciesWorth[CurrencyType.Gold];

            int totalCopperWorth = copperWorth * wallet[CurrencyType.Copper];
            int totalSilverWorth = silverWorth * wallet[CurrencyType.Silver];
            int totalGoldWorth = goldWorth * wallet[CurrencyType.Gold];

            List<Dictionary<CurrencyType, int>> possibleResults = GetPossibleConversions(
                wallet,
                totalCopperWorth, totalSilverWorth, totalGoldWorth,
                copperWorth, silverWorth, goldWorth);

            return possibleResults[Random.Range(0, possibleResults.Count)];
        }

        List<Dictionary<CurrencyType, int>> GetPossibleConversions(
            Dictionary<CurrencyType, int> wallet,
            int copperTotalWorth, int silverTotalWorth, int goldTotalWorth,
            int copperWorth, int silverWorth, int goldWorth)
        {
            List<Dictionary<CurrencyType, int>> wallets = new();

            if (copperTotalWorth % silverWorth == 0)
                GetWalletWithOneCurrency(
                    CurrencyType.Copper, CurrencyType.Silver,
                    copperTotalWorth, silverWorth);
            else if (copperTotalWorth % silverWorth % goldWorth == 0)
                GetWalletWithTwoCurrencies(
                    CurrencyType.Copper, CurrencyType.Silver, CurrencyType.Gold,
                    copperTotalWorth, silverWorth, goldWorth);

            if (copperTotalWorth % goldWorth == 0)
                GetWalletWithOneCurrency(
                    CurrencyType.Copper, CurrencyType.Gold,
                    copperTotalWorth, goldWorth);
            else if (copperTotalWorth % goldWorth % silverWorth == 0)
                GetWalletWithTwoCurrencies(
                    CurrencyType.Copper, CurrencyType.Gold, CurrencyType.Silver,
                    copperTotalWorth, goldWorth, silverWorth);

            if (silverTotalWorth % copperWorth == 0)
                GetWalletWithOneCurrency(
                    CurrencyType.Silver, CurrencyType.Copper,
                    silverTotalWorth, copperWorth);
            else if (silverTotalWorth % copperWorth % goldWorth == 0)
                GetWalletWithTwoCurrencies(
                    CurrencyType.Silver, CurrencyType.Copper, CurrencyType.Gold,
                    silverTotalWorth, copperWorth, goldWorth);

            if (silverTotalWorth % goldWorth == 0)
                GetWalletWithOneCurrency(
                    CurrencyType.Silver, CurrencyType.Gold,
                    silverTotalWorth, goldWorth);
            else if (silverTotalWorth % goldWorth % copperWorth == 0)
                GetWalletWithTwoCurrencies(
                    CurrencyType.Silver, CurrencyType.Gold, CurrencyType.Copper,
                    silverTotalWorth, goldWorth, copperWorth);

            if (goldTotalWorth % copperWorth == 0)
                GetWalletWithOneCurrency(
                    CurrencyType.Gold, CurrencyType.Copper,
                    goldTotalWorth, copperWorth);
            else if (goldTotalWorth % copperWorth % silverWorth == 0)
                GetWalletWithTwoCurrencies(
                    CurrencyType.Gold, CurrencyType.Copper, CurrencyType.Silver,
                    goldTotalWorth, copperWorth, silverWorth);

            if (goldTotalWorth % silverWorth == 0)
                GetWalletWithOneCurrency(
                    CurrencyType.Gold, CurrencyType.Silver,
                    goldTotalWorth, silverWorth);
            else if (goldTotalWorth % silverWorth % copperWorth == 0)
                GetWalletWithTwoCurrencies(
                    CurrencyType.Gold, CurrencyType.Silver, CurrencyType.Copper,
                    goldTotalWorth, silverWorth, copperWorth);

            return wallets;

            void GetWalletWithOneCurrency(
                CurrencyType fromCurrencyType, CurrencyType toCurrencyType,
                int fromTotalWorth, int toWorth)
            {
                Dictionary<CurrencyType, int> copy = wallet.GetCopy();
                copy[fromCurrencyType] = 0;
                copy[toCurrencyType] += fromTotalWorth / toWorth;
                wallets.Add(copy);
            }

            void GetWalletWithTwoCurrencies(
                CurrencyType fromCurrencyType,
                CurrencyType toCurrencyType1, CurrencyType toCurrencyType2,
                int fromTotalWorth, int toWorth1, int toWorth2)
            {
                Dictionary<CurrencyType, int> copy = wallet.GetCopy();
                copy[fromCurrencyType] = 0;
                copy[toCurrencyType1] += fromTotalWorth / toWorth1;
                copy[toCurrencyType2] += fromTotalWorth % toWorth1 / toWorth2;
                wallets.Add(copy);
            }
        }
    }
}
