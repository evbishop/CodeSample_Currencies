using CodeSample_Currency.Currency;
using CodeSample_Currency.SaveData;
using CodeSample_Currency.Utility;
using System.Collections.Generic;

namespace CodeSample_Currency
{
    public class CurrencyHandler : MonoSingleton<CurrencyHandler>
    {
        public delegate void WalletUpdateDelegate(
            CurrencyType currencyType,
            int newQuantity,
            bool animateChange);
        public event WalletUpdateDelegate OnWalletUpdated;

        SaveDataCurrency saveData = new();

        public bool IsDirty { get; set; }

        public Dictionary<CurrencyType, int> CurrenciesWorth => saveData.CurrenciesWorth;
        public Dictionary<CurrencyType, int> Wallet => saveData.Wallet;

        void Awake()
        {
            if (TryInitializeSingleton())
            {
                DontDestroyOnLoad(gameObject);
                saveData.Load();
            }
        }

        public void InitNew(int initialMoneyTotal)
        {
            saveData.Reset();

            Dictionary<CurrencyType, int> moneyInRandomCurrencies =
                CurrencyHelper.Instance.GetMoneyInRandomCurrencies(CurrenciesWorth, initialMoneyTotal);

            foreach (var (currencyType, quantity) in moneyInRandomCurrencies)
            {
                Wallet[currencyType] = quantity;
                OnWalletUpdated?.Invoke(currencyType, Wallet[currencyType], false);
            }

            IsDirty = true;
            Save();
        }

        #region SaveLoad
        public void Save()
        {
            //await UniTask.Yield(); // Simple protection from
            if (IsDirty)             // saving multiple times in a single frame
            {
                IsDirty = false;
                saveData.Save();
            }
        }

        public void Load()
        {
            foreach (var (currencyType, quantity) in Wallet)
                OnWalletUpdated?.Invoke(currencyType, quantity, false);
        }
        #endregion

        public void AddMoney(int money, bool save)
        {
            Dictionary<CurrencyType, int> moneyInRandomCurrencies =
                CurrencyHelper.Instance.GetMoneyInRandomCurrencies(CurrenciesWorth, money);

            foreach (var (currencyType, quantity) in moneyInRandomCurrencies)
                AddCurrency(currencyType, quantity, false);

            if (save)
            {
                IsDirty = true;
                Save();
            }
        }

        public void AddCurrency(CurrencyType currencyType, int quantity, bool save)
        {
            if (quantity == 0)
                return;

            Wallet[currencyType] += quantity;
            OnWalletUpdated?.Invoke(currencyType, Wallet[currencyType], true);

            if (save)
            {
                IsDirty = true;
                Save();
            }
        }

        public void RemoveCurrencies(Dictionary<CurrencyType, int> currenciesToRemove)
        {
            foreach (var (currencyType, quantity) in currenciesToRemove)
            {
                Wallet[currencyType] -= quantity;
                if (quantity != 0)
                    IsDirty = true;

                OnWalletUpdated?.Invoke(currencyType, Wallet[currencyType], false);
            }

            Save();
        }
    }
}
