using CodeSample_Currency.Currency;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeSample_Currency.CurrencyTests
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

        protected bool AreDictsEqual(Dictionary<CurrencyType, int> dict1, Dictionary<CurrencyType, int> dict2)
        {
            return dict1.All(keyValue => keyValue.Value == dict2[keyValue.Key]);
        }

        protected void PrintDebug(Dictionary<CurrencyType, int> result, string caller, bool isFailed)
        {
            string resultString = "";
            foreach (var (currencyType, quantity) in result)
            {
                resultString += $"\n{currencyType}: {quantity}";
            }

            if (isFailed)
                Debug.LogError($"{caller} failed on:{resultString}\n");
            else
                Debug.LogWarning($"{caller} never got a result:{resultString}\n");
        }
    }
}
