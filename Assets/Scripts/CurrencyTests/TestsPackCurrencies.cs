using CodeSample_Currency.Currency;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSample_Currency.CurrencyTests
{
    public class TestsPackCurrencies : MonoBehaviour
    {
        CurrencyHelper helper;

        [Test]
        public void _Init()
        {
            helper = CurrencyHelper.Instance;
            Assert.IsTrue(true);
        }

        [Test]
        public void Pack_Copper_Into_Silver()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 5 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> worth = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 5 },
                { CurrencyType.Gold, 10 },
            };

            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 1 },
            };

            Dictionary<CurrencyType, int> actualResult = helper
                .PackTreeCurrenciesIntoTwo(wallet, worth);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Pack_Copper_Into_Gold()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 10 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> worth = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 4 },
                { CurrencyType.Gold, 10 },
            };

            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 2 },
            };

            Dictionary<CurrencyType, int> actualResult = helper
                .PackTreeCurrenciesIntoTwo(wallet, worth);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Pack_Copper_Into_Gold_And_Silver()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 15 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> worth = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 5 },
                { CurrencyType.Gold, 10 },
            };

            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 0 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 2 },
            };

            Dictionary<CurrencyType, int> actualResult = helper
                .PackTreeCurrenciesIntoTwo(wallet, worth);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Pack_Silver_Into_Gold()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 4 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> worth = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 5 },
                { CurrencyType.Gold, 10 },
            };

            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 3 },
            };

            Dictionary<CurrencyType, int> actualResult = helper
                .PackTreeCurrenciesIntoTwo(wallet, worth);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Pack_Silver_Into_Copper()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> worth = new()
            {
                { CurrencyType.Copper, 2 },
                { CurrencyType.Silver, 4 },
                { CurrencyType.Gold, 7 },
            };

            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 3 },
                { CurrencyType.Silver, 0 },
                { CurrencyType.Gold, 1 },
            };

            Dictionary<CurrencyType, int> actualResult = helper
                .PackTreeCurrenciesIntoTwo(wallet, worth);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Pack_Gold_Into_Silver()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> worth = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 5 },
                { CurrencyType.Gold, 10 },
            };

            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 3 },
                { CurrencyType.Gold, 0 },
            };

            Dictionary<CurrencyType, int> actualResult = helper
                .PackTreeCurrenciesIntoTwo(wallet, worth);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Pack_Gold_Into_Silver_And_Copper()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> worth = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 5 },
                { CurrencyType.Gold, 7 },
            };

            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 3 },
                { CurrencyType.Silver, 2 },
                { CurrencyType.Gold, 0 },
            };

            Dictionary<CurrencyType, int> actualResult = helper
                .PackTreeCurrenciesIntoTwo(wallet, worth);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Pack_Gold_Into_Copper()
        {
            Dictionary<CurrencyType, int> wallet = new()
            {
                { CurrencyType.Copper, 1 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 1 },
            };
            Dictionary<CurrencyType, int> worth = new()
            {
                { CurrencyType.Copper, 3 },
                { CurrencyType.Silver, 4 },
                { CurrencyType.Gold, 9 },
            };

            Dictionary<CurrencyType, int> expectedResult = new()
            {
                { CurrencyType.Copper, 4 },
                { CurrencyType.Silver, 1 },
                { CurrencyType.Gold, 0 },
            };

            Dictionary<CurrencyType, int> actualResult = helper
                .PackTreeCurrenciesIntoTwo(wallet, worth);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
