using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeSample_Currencies.Currency
{
    public class PanelCurrency : MonoBehaviour
    {
        [SerializeField] CurrencyType currencyType;
        [SerializeField] Image currencyImage;
        [SerializeField] TMP_Text currencyText;
        [SerializeField] DOTweenAnimation currencyAnimation; // doing this animation with DOTween
                                                             // rather than Doozy just to show that
                                                             // I can do both

        void Start()
        {
            currencyImage.sprite = CurrencyHelper.Instance.CurrencyInfos[currencyType].Sprite;
            CurrencyHandler.Instance.OnWalletUpdated += HandleCurrencyUpdated;
        }

        void OnDestroy()
        {
            if (CurrencyHandler.Instance is not null)
                CurrencyHandler.Instance.OnWalletUpdated -= HandleCurrencyUpdated;
        }

        [Button]
        void PlayAnimation()
        {
            currencyAnimation.DORestart();
            currencyAnimation.DOPlay();
        }

        void HandleCurrencyUpdated(CurrencyType currencyType, int newQuantity, bool animateChange)
        {
            if (currencyType != this.currencyType)
                return;

            currencyText.text = $"x{newQuantity}";
            if (animateChange)
                PlayAnimation();
        }
    }
}
