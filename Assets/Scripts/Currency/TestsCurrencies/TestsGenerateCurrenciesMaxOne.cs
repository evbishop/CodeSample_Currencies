using NUnit.Framework;
using System.Collections.Generic;

namespace CodeSample_Currencies.Currency.TestsCurrencies
{
    public class TestsGenerateCurrenciesMaxOne : TestsGenerateCurrencies
    {
        protected override Dictionary<CurrencyType, int> GetActualResult(int worth)
        {
            return helper.GetMoneyInRandomCurrency(
                currenciesWorth,
                worth);
        }

        [Test]
        public void Generate_0()
        {
            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 0 },
            };

            Assert.IsTrue(Run(0, nameof(Generate_0),
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

            Assert.IsTrue(Run(1, nameof(Generate_1),
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

            Assert.IsTrue(Run(2, nameof(Generate_2),
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

            Assert.IsTrue(Run(3, nameof(Generate_3),
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

            Assert.IsTrue(Run(4, nameof(Generate_4),
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

            Assert.IsTrue(Run(5, nameof(Generate_5),
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

            Assert.IsTrue(Run(6, nameof(Generate_6),
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

            Assert.IsTrue(Run(7, nameof(Generate_7),
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

            Assert.IsTrue(Run(8, nameof(Generate_8),
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

            Assert.IsTrue(Run(9, nameof(Generate_9),
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

            Assert.IsTrue(Run(13, nameof(Generate_13),
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

            Assert.IsTrue(Run(14, nameof(Generate_14),
                expectedResult1, expectedResult2, expectedResult3));
        }
    }
}
