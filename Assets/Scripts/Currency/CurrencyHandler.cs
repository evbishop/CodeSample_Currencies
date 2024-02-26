using CodeSample_Currencies.Utility;
using System.Collections.Generic;
using System.Linq;

namespace CodeSample_Currencies.Currency
{
    public class CurrencyHandler : MonoSingleton<CurrencyHandler>
    {
        public delegate void WalletUpdateDelegate(
            CurrencyType currencyType,
            int newQuantity,
            bool animateChange);
        public event WalletUpdateDelegate OnWalletUpdated;

        public Dictionary<CurrencyType, int> CurrenciesWorth { get; private set; }
        public Dictionary<CurrencyType, int> Wallet { get; private set; }

        void Awake()
        {
            if (TryInitializeSingleton())
            {
                Wallet = CurrencyHelper.Instance.GetEmptyWallet();
                CurrenciesWorth = CurrencyHelper.Instance.GetAllCurrenciesDefaultWorth();
            }
        }

        public void InitNew(int initialMoneyTotal)
        {
            Dictionary<CurrencyType, int> moneyInRandomCurrencies =
                CurrencyHelper.Instance.GetMoneyInRandomCurrencies(CurrenciesWorth, initialMoneyTotal);

            foreach (var (currencyType, quantity) in moneyInRandomCurrencies)
            {
                Wallet[currencyType] = quantity;
                OnWalletUpdated?.Invoke(currencyType, Wallet[currencyType], false);
            }
        }

        public void AddMoney(int money, bool maxTwo)
        {
            Dictionary<CurrencyType, int> moneyInRandomCurrencies =
                CurrencyHelper.Instance.GetMoneyInRandomCurrencies(CurrenciesWorth, money, maxTwo);

            foreach (var (currencyType, quantity) in moneyInRandomCurrencies)
                AddCurrency(currencyType, quantity);
        }

        public void AddCurrency(CurrencyType currencyType, int quantity)
        {
            if (quantity == 0)
                return;

            Wallet[currencyType] += quantity;
            OnWalletUpdated?.Invoke(currencyType, Wallet[currencyType], true);
        }

        public bool TryRemoveCurrencies(Dictionary<CurrencyType, int> currenciesToRemove)
        {
            if (currenciesToRemove.Any(currency => currency.Value > Wallet[currency.Key]))
                return false;

            foreach (var (currencyType, quantity) in currenciesToRemove)
            {
                Wallet[currencyType] -= quantity;

                OnWalletUpdated?.Invoke(currencyType, Wallet[currencyType], false);
            }
            return true;
        }
    }
}
