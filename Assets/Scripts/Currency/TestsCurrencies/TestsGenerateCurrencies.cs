using System;
using System.Collections.Generic;

namespace CodeSample_Currencies.Currency.TestsCurrencies
{
    public abstract class TestsGenerateCurrencies : TestsCurrencies
    {
        protected bool Run(int worth, string caller,
            params Dictionary<CurrencyType, int>[] expectedResults)
        {
            Func<Dictionary<CurrencyType, int>> getActualResult = () =>
            {
                return GetActualResult(worth);
            };

            return Run(getActualResult, caller, expectedResults);
        }

        protected abstract Dictionary<CurrencyType, int> GetActualResult(int worth);
    }
}
