using UnityEngine;

namespace CodeSample_Currencies.Currency
{
    public class CurrencyTag : MonoBehaviour
    {
        [field: SerializeField] public CurrencyType CurrencyType { get; set; }
    }
}
