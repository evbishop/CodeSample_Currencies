using CodeSample_Currencies.Currency;
using Doozy.Runtime.Common.Extensions;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using TMPro;
using UnityEngine;

namespace CodeSample_Currencies
{
    public class PanelAddMoney : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputFieldMoneyToAdd;
        [SerializeField] TMP_Text textPlaceholderInputFieldMoneyToAdd;
        [SerializeField] TMP_Text textCurrenciesQuantityInfo;
        [SerializeField] UIStepper stepperCurrenciesQuantity;
        [SerializeField] UIContainer containerCurrencySelection;
        [SerializeField] UIToggleGroup toggleGroupCurrencyTypes;

        // Called from Button - Add Money
        public void AddMoney()
        {
            int money = 0;
            if (inputFieldMoneyToAdd.text != "")
                money = int.Parse(inputFieldMoneyToAdd.text);

            if (stepperCurrenciesQuantity.value == 1)
            {
                if (toggleGroupCurrencyTypes.firstToggleOn.TryGetComponent<CurrencyTag>(
                    out var currencyTag))
                {
                    CurrencyHandler.Instance.AddCurrency(currencyTag.CurrencyType, money);
                }
                else
                {
                    Debug.LogError($"{toggleGroupCurrencyTypes.firstToggleOn.gameObject.name} is missing a CurrencyTag");
                }
            }
            else
            {
                CurrencyHandler.Instance.AddMoney(money, stepperCurrenciesQuantity.value == 2);
            }
        }

        // Called from UIStepper OnValueChanged callback
        public void HandleCurrenciesQuantityChanged()
        {
            if (stepperCurrenciesQuantity.value.Approximately(1))
            {
                containerCurrencySelection.Show();
                inputFieldMoneyToAdd.text = "";
                textPlaceholderInputFieldMoneyToAdd.text = "Enter coins quantity";
            }
            else
            {
                containerCurrencySelection.Hide();
                inputFieldMoneyToAdd.text = "";
                textPlaceholderInputFieldMoneyToAdd.text = "Enter total money worth amount";
            }
        }
    }
}
