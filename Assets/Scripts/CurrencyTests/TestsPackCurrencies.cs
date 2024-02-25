using CodeSample_Currencies.Currency;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CodeSample_Currencies.TestsCurrencies
{
    public class TestsPackCurrencies : TestsCurrencies
    {
        bool Run(Dictionary<CurrencyType, int> wallet, string caller,
            params Dictionary<CurrencyType, int>[] expectedResults)
        {
            bool isPassingAll = true;
            int iterations = 10000;
            HashSet<Dictionary<CurrencyType, int>> unachievedResults = expectedResults.ToHashSet();
            Dictionary<string, int> resultCounts = new();

            for (int i = 0; i < iterations; i++)
            {
                Dictionary<CurrencyType, int> actualResult;

                actualResult = helper.PackTreeCurrenciesIntoTwo(wallet, currenciesWorth);

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

        [Test]
        public void Pack_0()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };

            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_0),
                expectedResult));
        }

        [Test]
        public void Pack_Copper_1()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };

            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_Copper_1),
                expectedResult));
        }

        [Test]
        public void Pack_Silver_1()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };

            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 3 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_Silver_1),
                expectedResult1, expectedResult2));
        }

        [Test]
        public void Pack_Gold_1()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };

            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 5 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 2 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_Gold_1),
                expectedResult1, expectedResult2, expectedResult3));
        }

        [Test]
        public void Pack_Copper_Silver()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };

            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 4 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_Copper_Silver),
                expectedResult1, expectedResult2));
        }

        [Test]
        public void Pack_Copper_Gold()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };

            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 6 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 3 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_Copper_Gold),
                expectedResult1, expectedResult2, expectedResult3));
        }

        [Test]
        public void Pack_Silver_Gold()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };

            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 3 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 5 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 2 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_Silver_Gold),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4));
        }

        [Test]
        public void Pack_Copper_Silver_2()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 20 },
                { CurrencyType.Silver, 20 },
                { CurrencyType.Gold, 0 },
            };

            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 20 },
                { CurrencyType.Silver, 20 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 20 },
                { CurrencyType.Gold, 4 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 80 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 20 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 12 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_Copper_Silver_2),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4));
        }

        [Test]
        public void Pack_Copper_Silver_3()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 23 },
                { CurrencyType.Silver, 23 },
                { CurrencyType.Gold, 0 },
            };

            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 23 },
                { CurrencyType.Silver, 23 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 92 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 27 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 13 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 24 },
                { CurrencyType.Gold, 4 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_Copper_Silver_3),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4));
        }

        [Test]
        public void Pack_Copper_Gold_2()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 20 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 20 },
            };

            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 20 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 20 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 24 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 120 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 21 },
                { CurrencyType.Silver, 33 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_Copper_Gold_2),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4));
        }

        [Test]
        public void Pack_Copper_Gold_3()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 23 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 23 },
            };

            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 23 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 23 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 27 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 138 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 24 },
                { CurrencyType.Silver, 38 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_Copper_Gold_3),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4));
        }

        [Test]
        public void Pack_Silver_Gold_2()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 20 },
                { CurrencyType.Gold, 20 },
            };

            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 20 },
                { CurrencyType.Gold, 20 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 60 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 20 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 32 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 100 },
                { CurrencyType.Silver, 20 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult5 = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 53 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_Silver_Gold_2),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4,
                expectedResult5));
        }

        [Test]
        public void Pack_Silver_Gold_3()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 23 },
                { CurrencyType.Gold, 23 },
            };

            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 23 },
                { CurrencyType.Gold, 23 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 69 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 23 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 4 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 36 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 115 },
                { CurrencyType.Silver, 23 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult5 = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 61 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_Silver_Gold_3),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4,
                expectedResult5));
        }

        [Test]
        public void Pack_All()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 100 },
                { CurrencyType.Silver, 100 },
                { CurrencyType.Gold, 100 },
            };

            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 100 },
                { CurrencyType.Gold, 120 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 400 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 100 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 100 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 160 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 600 },
                { CurrencyType.Silver, 100 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult5 = new()
            {
                { CurrencyType.Copper, 102 },
                { CurrencyType.Silver, 266 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(wallet, nameof(Pack_All),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4,
                expectedResult5));
        }
    }
}
