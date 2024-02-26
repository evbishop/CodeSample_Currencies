using CodeSample_Currencies.Currency;

namespace CodeSample_Currencies.Utility
{
    public class GameSession : MonoSingleton<GameSession>
    {
        private void Awake()
        {
            if (TryInitializeSingleton())
            {
                PopupHelper.Instance.Init();
                CurrencyHelper.Instance.Init();
            }
        }

        private void OnApplicationQuit()
        {
            PopupHelper.Instance.HandleApplicationQuit();
        }
    }
}
