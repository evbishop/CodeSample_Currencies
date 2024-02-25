using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSample_Currencies.Currency
{
    public static class DictionaryExtensions
    {
        public static bool IsEqualByValues<T1>(this Dictionary<T1, int> dict1, Dictionary<T1, int> dict2)
            where T1 : Enum
        {
            return dict1.All(keyValue => keyValue.Value == dict2[keyValue.Key]);
        }

        public static string ToWalletString(this Dictionary<CurrencyType, int> wallet)
        {
            string result = "( ";
            foreach (var (currencyType, quantity) in wallet)
                result += $"{quantity} ";
            result += ")";
            return result;
        }

        public static Dictionary<CurrencyType, int> GetCopy(this Dictionary<CurrencyType, int> wallet)
        {
            Dictionary<CurrencyType, int> copy = new();
            foreach (var (currencyType, quantity) in wallet)
                copy.Add(currencyType, quantity);
            return copy;
        }
    }
}
