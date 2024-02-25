using CodeSample_Currencies.Currency;
using System.Collections.Generic;

namespace CodeSample_Currencies.SaveData
{
    public class SaveDataCurrency : SaveData
    {
        public Dictionary<CurrencyType, int> Wallet { get; private set; } = new();
        public Dictionary<CurrencyType, int> CurrenciesWorth { get; private set; } = new();

        public override void Save()
        {
            ES3.Save(nameof(Wallet), Wallet, Path);
            ES3.Save(nameof(CurrenciesWorth), CurrenciesWorth, Path);
        }

        public override void Load()
        {
            Wallet = ES3.Load(
                nameof(Wallet),
                Path,
                CurrencyHelper.Instance.GetEmptyWallet());

            CurrenciesWorth = ES3.Load(
                nameof(CurrenciesWorth),
                Path,
                CurrencyHelper.Instance.GetAllCurrenciesDefaultWorth());
        }

        public void Reset()
        {
            Wallet = CurrencyHelper.Instance.GetEmptyWallet();
            CurrenciesWorth = CurrencyHelper.Instance.GetAllCurrenciesDefaultWorth();
        }
    }
}
