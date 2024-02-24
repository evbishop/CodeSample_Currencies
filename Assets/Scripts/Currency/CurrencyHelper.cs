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

            Dictionary<CurrencyType, int> possibleCurrencies = new();
            foreach (var (currencyType, worth) in currenciesWorth)
            {
                if (worth <= moneyToGet)
                    possibleCurrencies.Add(currencyType, worth);
            }

            if (possibleCurrencies.Count == 0)
                return wallet;

            int randomIndex = Random.Range(0, possibleCurrencies.Count);
            var selectedCurrency = possibleCurrencies.Skip(randomIndex).First();

            FillWalletWithCurrency(wallet, currenciesWorth, selectedCurrency.Key, moneyToGet);

            return wallet;
        }

        public Dictionary<CurrencyType, int> GetMoneyInRandomCurrencies(
            Dictionary<CurrencyType, int> currenciesWorth, int targetWorth, bool maxTwo = false)
        {
            var result = GetEmptyWallet();

            Dictionary<CurrencyType, int> possibleCurrencies = new();
            foreach (var (currencyType, worth) in currenciesWorth)
                possibleCurrencies.Add(currencyType, worth);

            int generatedWorth = 0;
            while (generatedWorth < targetWorth)
            {
                if (generatedWorth + possibleCurrencies.Min(resource => resource.Value) > targetWorth)
                    break;

                int randomIndex = Random.Range(0, possibleCurrencies.Count - 1);
                var selectedCurrency = possibleCurrencies.Skip(randomIndex).First();

                if (generatedWorth + selectedCurrency.Value > targetWorth)
                    continue;

                generatedWorth += selectedCurrency.Value;

                result[selectedCurrency.Key]++;
            }

            if (maxTwo && result.Values.All(quantity => quantity > 0))
            {
                result = PackTreeCurrenciesIntoTwo(result, possibleCurrencies);
            }

            return result;
        }

        public void FillWalletWithCurrency(Dictionary<CurrencyType, int> wallet,
            Dictionary<CurrencyType, int> currenciesWorth,
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
