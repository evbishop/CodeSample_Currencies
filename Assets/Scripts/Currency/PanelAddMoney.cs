using CodeSample_Currencies.Currency;
using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeSample_Currencies
{
    public class PanelAddMoney : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputFieldMoneyToAdd;
        [SerializeField] TMP_InputField inputFieldCurrenciesQuantity;

        // Called from Button - Add Money
        public void AddMoney()
        {
            int money = 0;
            if (inputFieldMoneyToAdd.text != "")
                money = int.Parse(inputFieldMoneyToAdd.text);

            int currenciesQuantity = 1;
            if (inputFieldCurrenciesQuantity.text != "")
                currenciesQuantity = int.Parse(inputFieldCurrenciesQuantity.text);
            if (currenciesQuantity < 1)
            {
                print("Setting the minimum currencies quantity to add");
                currenciesQuantity = 1;
            }
            else if (currenciesQuantity > CurrencyHelper.Instance.CurrencyInfos.Count)
            {
                print("Setting the maximum currencies quantity to add");
                currenciesQuantity = CurrencyHelper.Instance.CurrencyInfos.Count;
            }
            inputFieldCurrenciesQuantity.text = currenciesQuantity.ToString();

            if (currenciesQuantity == 1)
            {
                CurrencyType randomType = (CurrencyType)Random.Range(
                    1,
                    Enum.GetNames(typeof(CurrencyType)).Length);
                CurrencyHandler.Instance.AddCurrency(randomType, money);
            }
            else
            {
                CurrencyHandler.Instance.AddMoney(money, currenciesQuantity == 2);
            }
        }
    }
}
