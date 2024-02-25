using CodeSample_Currencies.Currency;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeSample_Currencies.TestsCurrencies
{
    public abstract class TestsCurrencies
    {
        protected CurrencyHelper helper;
        protected Dictionary<CurrencyType, int> currenciesWorth;

        [Test]
        public void _Init()
        {
            helper = CurrencyHelper.Instance;
            currenciesWorth = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 3 },
                { CurrencyType.Gold, 5 }
            };
            Assert.IsTrue(true);
        }

        protected void PrintDebug(Dictionary<CurrencyType, int> result, string caller, bool isFailed)
        {
            if (isFailed)
                Debug.LogError($"{caller} failed on: {result.ToWalletString()}\n");
            else
                Debug.LogWarning($"{caller} never got a result: {result.ToWalletString()}\n");
        }

        protected void PrintDebug(Dictionary<string, int> results, int iterations)
        {
            Dictionary<string, int> ordered = results
                .OrderByDescending(kvp => kvp.Value)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (var (key, value) in ordered)
                Debug.Log($"{key}: {(float)value / iterations * 100}%");
        }
    }
}
