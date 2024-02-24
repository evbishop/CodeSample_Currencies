using CodeSample_Currency.Currency;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeSample_Currency.CurrencyTests
{
    public class TestsPackCurrencies
    {
        CurrencyHelper helper;
        Dictionary<CurrencyType, int> currenciesWorth;

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

        bool Run(Dictionary<CurrencyType, int> wallet, string caller,
            params Dictionary<CurrencyType, int>[] expectedResults)
        {
            bool isPassingAll = true;
            HashSet<Dictionary<CurrencyType, int>> unachievedResults = expectedResults.ToHashSet();
            for (int i = 0; i < 10000; i++)
            {
                Dictionary<CurrencyType, int> actualResult;

                actualResult = helper.PackTreeCurrenciesIntoTwo(wallet, currenciesWorth);

                bool isPassing = expectedResults.Any(expectedResult =>
                    AreDictsEqual(expectedResult, actualResult));
                if (isPassing)
                {
                    var unachievedResult = unachievedResults
                        .FirstOrDefault(result =>
                            AreDictsEqual(result, actualResult));
                    if (unachievedResult is not null)
                        unachievedResults.Remove(unachievedResult);
                }
                else
                {
                    isPassingAll = false;
                    PrintDebug(actualResult, caller, true);
                }
            }

            foreach (var result in unachievedResults)
            {
                PrintDebug(result, caller, false);
            }

            return isPassingAll;
        }

        bool AreDictsEqual(Dictionary<CurrencyType, int> dict1, Dictionary<CurrencyType, int> dict2)
        {
            return dict1.All(keyValue => keyValue.Value == dict2[keyValue.Key]);
        }

        void PrintDebug(Dictionary<CurrencyType, int> result, string caller, bool isFailed)
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

            Assert.IsTrue(Run(wallet, nameof(Pack_Copper_1),
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
    }
}
