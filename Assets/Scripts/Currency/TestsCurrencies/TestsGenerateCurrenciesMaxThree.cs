using NUnit.Framework;
using System.Collections.Generic;

namespace CodeSample_Currencies.Currency.TestsCurrencies
{
    public class TestsGenerateCurrenciesMaxThree : TestsGenerateCurrencies
    {
        protected override Dictionary<CurrencyType, int> GetActualResult(int worth)
        {
            return helper.GetMoneyInRandomCurrencies(
                currenciesWorth,
                worth);
        }

        [Test]
        public void Generate_5_Three_Allowed()
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

            Assert.IsTrue(Run(5, nameof(Generate_5_Three_Allowed),
                expectedResult1, expectedResult2, expectedResult3));
        }

        [Test]
        public void Generate_6_Three_Allowed()
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

            Assert.IsTrue(Run(6, nameof(Generate_6_Three_Allowed),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4));
        }

        [Test]
        public void Generate_7_Three_Allowed()
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

            Assert.IsTrue(Run(7, nameof(Generate_7_Three_Allowed),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4));
        }

        [Test]
        public void Generate_8_Three_Allowed()
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

            Assert.IsTrue(Run(8, nameof(Generate_8_Three_Allowed),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4,
                expectedResult5));
        }

        [Test]
        public void Generate_9_Three_Allowed()
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
            Dictionary<CurrencyType, int> expectedResult6 = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };

            Assert.IsTrue(Run(9, nameof(Generate_9_Three_Allowed),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4,
                expectedResult5, expectedResult6));
        }

        [Test]
        public void Generate_13_Three_Allowed()
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
            Dictionary<CurrencyType, int> expectedResult9 = new()
            {
                { CurrencyType.Copper, 5 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> expectedResult10 = new()
            {
                { CurrencyType.Copper, 2 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 1 },
            };

            Assert.IsTrue(Run(13, nameof(Generate_13_Three_Allowed),
                expectedResult1, expectedResult2, expectedResult3, expectedResult4,
                expectedResult5, expectedResult6, expectedResult7, expectedResult8,
                expectedResult9, expectedResult10));
        }
    }
}
