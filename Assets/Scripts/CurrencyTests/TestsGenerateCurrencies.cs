using CodeSample_Currency.Currency;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeSample_Currency.CurrencyTests
{
    public class TestsGenerateCurrencies
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

        bool Run(int maxCurrenciesToGenerate, int worth, string caller,
            params Dictionary<CurrencyType, int>[] expectedResults)
        {
            bool isPassingAll = true;
            HashSet<Dictionary<CurrencyType, int>> unachievedResults = expectedResults.ToHashSet();
            for (int i = 0; i < 10000; i++)
            {
                Dictionary<CurrencyType, int> actualResult;

                if (maxCurrenciesToGenerate == 1)
                {
                    actualResult = helper.GetMoneyInRandomCurrency(
                        currenciesWorth,
                        worth);
                }
                else if (maxCurrenciesToGenerate == 2)
                {
                    actualResult = helper.GetMoneyInRandomCurrencies(
                        currenciesWorth,
                        worth,
                        true);
                }
                else
                {
                    Debug.LogWarning($"No code to test generation of {maxCurrenciesToGenerate} currencies");
                    return false;
                }

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

        #region Generate money in a single currency
        [Test]
        public void Generate_0()
        {
            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(1, 0, nameof(Generate_0),
                expectedResult));
        }

        [Test]
        public void Generate_1()
        {
            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(1, 1, nameof(Generate_1),
                expectedResult));
        }

        [Test]
        public void Generate_2()
        {
            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 2 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(1, 2, nameof(Generate_2),
                expectedResult));
        }

        [Test]
        public void Generate_3()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 3 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(1, 3, nameof(Generate_3),
                expectedResult1, expectedResult2));
        }

        [Test]
        public void Generate_4()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 4 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(1, 4, nameof(Generate_4),
                expectedResult1, expectedResult2));
        }

        [Test]
        public void Generate_5()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 5 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };

            Assert.IsTrue(Run(1, 5, nameof(Generate_5),
                expectedResult1, expectedResult2, expectedResult3));
        }

        [Test]
        public void Generate_6()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 6 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };

            Assert.IsTrue(Run(1, 6, nameof(Generate_6),
                expectedResult1, expectedResult2, expectedResult3));
        }

        [Test]
        public void Generate_7()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 7 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };

            Assert.IsTrue(Run(1, 7, nameof(Generate_7),
                expectedResult1, expectedResult2, expectedResult3));
        }

        [Test]
        public void Generate_8()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 8 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 2 },
            };

            Assert.IsTrue(Run(1, 8, nameof(Generate_8),
                expectedResult1, expectedResult2, expectedResult3));
        }

        [Test]
        public void Generate_9()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 9 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 3 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 2 },
            };

            Assert.IsTrue(Run(1, 9, nameof(Generate_9),
                expectedResult1, expectedResult2, expectedResult3));
        }

        [Test]
        public void Generate_13()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 13 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 4 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 3 },
            };

            Assert.IsTrue(Run(1, 13, nameof(Generate_13),
                expectedResult1, expectedResult2, expectedResult3));
        }

        [Test]
        public void Generate_14()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 14 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 4 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 3 },
            };

            Assert.IsTrue(Run(1, 14, nameof(Generate_14),
                expectedResult1, expectedResult2, expectedResult3));
        }
        #endregion

        #region Generate money in two currencies

        [Test]
        public void Generate_5_Two_Allowed()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 5 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 2 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(2, 5, nameof(Generate_5_Two_Allowed),
                expectedResult1, expectedResult2, expectedResult3));
        }

        [Test]
        public void Generate_6_Two_Allowed()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 6 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 3 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(2, 6, nameof(Generate_6_Two_Allowed),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4));
        }

        [Test]
        public void Generate_7_Two_Allowed()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 7 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 2 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 4 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(2, 7, nameof(Generate_7_Two_Allowed),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4));
        }

        [Test]
        public void Generate_8_Two_Allowed()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 8 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 2 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 3 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> expectedResult5 = new()
            {
                { CurrencyType.Copper, 5 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(2, 8, nameof(Generate_8_Two_Allowed),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4,
                expectedResult5));
        }

        [Test]
        public void Generate_9_Two_Allowed()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 9 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 3 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 4 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 3 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult5 = new()
            {
                { CurrencyType.Copper, 6 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(2, 9, nameof(Generate_9_Two_Allowed),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4,
                expectedResult5));
        }

        [Test]
        public void Generate_13_Two_Allowed()
        {
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 13 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 4 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 3 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 2 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 2 },
            };
            Dictionary<CurrencyType, int> expectedResult5 = new()
            {
                { CurrencyType.Copper, 4 },
                { CurrencyType.Silver, 3 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult6 = new()
            {
                { CurrencyType.Copper, 7 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult7 = new()
            {
                { CurrencyType.Copper, 10 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult8 = new()
            {
                { CurrencyType.Copper, 8 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };

            Assert.IsTrue(Run(2, 13, nameof(Generate_13_Two_Allowed),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4,
                expectedResult5, expectedResult6, expectedResult7, expectedResult8));
        }
        #endregion
    }
}
