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

        bool AreDictsEqual(Dictionary<CurrencyType, int> dict1, Dictionary<CurrencyType, int> dict2, string caller)
        {
            bool areEqual = dict1.All(keyValue => keyValue.Value == dict2[keyValue.Key]);

            if (areEqual)
            {
                string message = caller;
                foreach (var (key, value) in dict1)
                {
                    message += $"\n{key}: {value}";
                }
                Debug.Log(message);
            }

            return areEqual;
        }

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
            Assert.AreEqual(true, true);
        }

        [Test]
        public void Generate_0()
        {
            int worth = 0;

            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, false);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Generate_1()
        {
            int worth = 1;
            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, false);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Generate_2()
        {
            int worth = 2;
            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 2 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, false);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Generate_3()
        {
            int worth = 3;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, false);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_3)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_3)));
        }

        [Test]
        public void Generate_4()
        {
            int worth = 4;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, false);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_4)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_4)));
        }

        [Test]
        public void Generate_5()
        {
            int worth = 5;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, false);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_5)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_5)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_5)));
        }

        [Test]
        public void Generate_6()
        {
            int worth = 6;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, false);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_6)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_6)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_6)));
        }

        [Test]
        public void Generate_7()
        {
            int worth = 7;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, false);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_7)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_7)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_7)));
        }

        [Test]
        public void Generate_8()
        {
            int worth = 8;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, false);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_8)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_8)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_8)));
        }

        [Test]
        public void Generate_9()
        {
            int worth = 9;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, false);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_9)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_9)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_9)));
        }

        [Test]
        public void Generate_13()
        {
            int worth = 13;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, false);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_13)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_13)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_13)));
        }

        [Test]
        public void Generate_14()
        {
            int worth = 14;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, false);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_14)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_14)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_14)));
        }

        #region Two Allowed

        [Test]
        public void Generate_5_Two_Allowed()
        {
            int worth = 5;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, true);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_5_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_5_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_5_Two_Allowed)));
        }

        [Test]
        public void Generate_6_Two_Allowed()
        {
            int worth = 6;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, true);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_6_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_6_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_6_Two_Allowed)));
        }

        [Test]
        public void Generate_7_Two_Allowed()
        {
            int worth = 7;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, true);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_7_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_7_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_7_Two_Allowed)));
        }

        [Test]
        public void Generate_8_Two_Allowed()
        {
            int worth = 8;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, true);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_8_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_8_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_8_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult4, nameof(Generate_8_Two_Allowed)));
        }

        [Test]
        public void Generate_9_Two_Allowed()
        {
            int worth = 9;
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
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, true);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_9_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_9_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_9_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult4, nameof(Generate_9_Two_Allowed)));
        }

        [Test]
        public void Generate_13_Two_Allowed()
        {
            int worth = 13;
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
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, true);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_13_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_13_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_13_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult4, nameof(Generate_13_Two_Allowed)));
        }

        [Test]
        public void Generate_10000009_Two_Allowed()
        {
            int worth = 10000009;
            Dictionary<CurrencyType, int> expectedResult1 = new()
            {
                { CurrencyType.Copper, 10000009 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult2 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 3333336 },
                { CurrencyType.Gold, 0 },
            };
            Dictionary<CurrencyType, int> expectedResult3 = new()
            {
                { CurrencyType.Copper, 4 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 2000001 },
            };
            Dictionary<CurrencyType, int> expectedResult4 = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 2000001 },
            };
            Dictionary<CurrencyType, int> actualResult = helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth, true);
            Assert.IsTrue(AreDictsEqual(actualResult, expectedResult1, nameof(Generate_10000009_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult2, nameof(Generate_10000009_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult3, nameof(Generate_10000009_Two_Allowed)) ||
                AreDictsEqual(actualResult, expectedResult4, nameof(Generate_10000009_Two_Allowed)));
        }
        #endregion
    }
}
