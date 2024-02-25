using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeSample_Currencies.Currency
{
    [CreateAssetMenu(fileName = "Currency", menuName = "Scriptable Objects/Currency")]
    public class CurrencyInfo : ScriptableObject
    {
        private const string CURRENCY = "Currency";
        private const string DATA = "Currency/Data";

        [field: SerializeField, HorizontalGroup(CURRENCY, 64), PreviewField(64), HideLabel]
        public Sprite Sprite { get; private set; }

        [field: SerializeField, VerticalGroup(DATA), LabelWidth(100)]
        public CurrencyType CurrencyType { get; private set; }

        [field: SerializeField, VerticalGroup(DATA), LabelWidth(100)]
        public string LocalizationKey { get; private set; } // since this is a sample project,
                                                            // I will just be using this key
                                                            // as the name of the currency to display,
                                                            // without passing it to any localization system

        [field: SerializeField, VerticalGroup(DATA), LabelWidth(100)]
        public int Worth { get; private set; }
    }
}