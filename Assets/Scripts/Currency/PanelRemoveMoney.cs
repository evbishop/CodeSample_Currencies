using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeSample_Currencies.Currency
{
    public class PanelRemoveMoney : MonoBehaviour
    {
        [SerializeField, ChildGameObjectsOnly] TMP_InputField inputFieldCopperCoinsToRemove;
        [SerializeField, ChildGameObjectsOnly] TMP_InputField inputFieldSilverCoinsToRemove;
        [SerializeField, ChildGameObjectsOnly] TMP_InputField inputFieldGoldCoinsToRemove;

        // Called from Button - Remove Coins
        public void RemoveCoins()
        {
            Dictionary<CurrencyType, int> currenciesToRemove = new();
            if (inputFieldCopperCoinsToRemove.text != "")
                currenciesToRemove.Add(CurrencyType.Copper, int.Parse(inputFieldCopperCoinsToRemove.text));
            if (inputFieldSilverCoinsToRemove.text != "")
                currenciesToRemove.Add(CurrencyType.Silver, int.Parse(inputFieldSilverCoinsToRemove.text));
            if (inputFieldGoldCoinsToRemove.text != "")
                currenciesToRemove.Add(CurrencyType.Gold, int.Parse(inputFieldGoldCoinsToRemove.text));

            CurrencyHandler.Instance.RemoveCurrencies(currenciesToRemove);
        }
    }
}
