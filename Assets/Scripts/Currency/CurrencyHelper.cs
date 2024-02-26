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

        public void Init() { }

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
            CurrencyType copperType = CurrencyType.Copper;
            CurrencyType silverType = CurrencyType.Silver;
            CurrencyType goldType = CurrencyType.Gold;

            CurrencyData copperData = new CurrencyData(
                copperType,
                currenciesWorth[copperType],
                currenciesWorth[copperType] * wallet[copperType]);
            CurrencyData silverData = new CurrencyData(
                silverType,
                currenciesWorth[silverType],
                currenciesWorth[silverType] * wallet[silverType]);
            CurrencyData goldData = new CurrencyData(
                goldType,
                currenciesWorth[goldType],
                currenciesWorth[goldType] * wallet[goldType]);

            List<Dictionary<CurrencyType, int>> possibleResults = GetPossibleConversions(
                wallet, copperData, silverData, goldData);

            return possibleResults[Random.Range(0, possibleResults.Count)];
        }

        List<Dictionary<CurrencyType, int>> GetPossibleConversions(
            Dictionary<CurrencyType, int> wallet,
            params CurrencyData[] currencies)
        {
            List<Dictionary<CurrencyType, int>> wallets = new();

            for (int i = 0; i < currencies.Length; i++)
            {
                CurrencyData currencyFrom = currencies[i];
                for (int j = 0; j < currencies.Length; j++)
                {
                    if (j == i)
                        continue;
                    CurrencyData currencyTo1 = currencies[j];
                    for (int k = 0; k < currencies.Length; k++)
                    {
                        if (k == i || k == j)
                            continue;
                        CurrencyData currencyTo2 = currencies[k];
                        if (currencyFrom.TotalWorth % currencyTo1.Worth == 0)
                        {
                            Dictionary<CurrencyType, int> copy = wallet.GetCopy();
                            copy[currencyFrom.CurrencyType] = 0;
                            copy[currencyTo1.CurrencyType] += currencyFrom.TotalWorth / currencyTo1.Worth;
                            wallets.Add(copy);
                        }
                        else if (currencyFrom.TotalWorth % currencyTo1.Worth % currencyTo2.Worth == 0)
                        {
                            Dictionary<CurrencyType, int> copy = wallet.GetCopy();
                            copy[currencyFrom.CurrencyType] = 0;
                            copy[currencyTo1.CurrencyType] += currencyFrom.TotalWorth / currencyTo1.Worth;
                            copy[currencyTo2.CurrencyType] += currencyFrom.TotalWorth % currencyTo1.Worth / currencyTo2.Worth;
                            wallets.Add(copy);
                        }
                    }
                }
            }

            return wallets;
        }

        struct CurrencyData
        {
            public CurrencyType CurrencyType;
            public int Worth;
            public int TotalWorth;

            public CurrencyData(CurrencyType currencyType, int worth, int totalWorth)
            {
                CurrencyType = currencyType;
                Worth = worth;
                TotalWorth = totalWorth;
            }
        }
    }
}
