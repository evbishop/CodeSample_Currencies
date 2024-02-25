using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeSample_Currencies.Currency.TestsCurrencies
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

        protected bool Run(Func<Dictionary<CurrencyType, int>> getActualResult, string caller,
            params Dictionary<CurrencyType, int>[] expectedResults)
        {
            bool isPassingAll = true;
            int iterations = 10000;
            HashSet<Dictionary<CurrencyType, int>> unachievedResults = expectedResults.ToHashSet();
            Dictionary<string, int> resultCounts = new();

            for (int i = 0; i < iterations; i++)
            {
                Dictionary<CurrencyType, int> actualResult = getActualResult();
                if (actualResult is null)
                    return false;

                var matchingResult = expectedResults
                    .FirstOrDefault(expectedResult =>
                        expectedResult.IsEqualByValues(actualResult));

                if (matchingResult is null)
                {
                    isPassingAll = false;
                    PrintDebug(actualResult, caller, true);
                }
                else
                {
                    var unachievedResult = unachievedResults
                        .FirstOrDefault(result =>
                            result.IsEqualByValues(actualResult));
                    if (unachievedResult is not null)
                        unachievedResults.Remove(unachievedResult);

                    string matchingResultString = matchingResult.ToWalletString();
                    if (resultCounts.ContainsKey(matchingResultString))
                        resultCounts[matchingResultString] += 1;
                    else
                        resultCounts.Add(matchingResultString, 1);
                }
            }

            PrintDebug(resultCounts, iterations);

            foreach (var result in unachievedResults)
                PrintDebug(result, caller, false);

            return isPassingAll;
        }
    }
}
