using Doozy.Runtime.UIManager.Containers;
using Doozy.Runtime.UIManager.ScriptableObjects;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSample_Currencies.Utility
{
    [CreateAssetMenu(fileName = "PopupHelper", menuName = "Scriptable Objects/PopupHelper")]
    public class PopupHelper : SerializedScriptableObject
    {
        #region Singleton

        private static string s_assetName => nameof(PopupHelper);
        private static string s_loadPath => $"Helpers/{s_assetName}";
        private static string s_savePath => $"Assets/Resources/{s_loadPath}.asset";

        private static PopupHelper s_instance;

        public static PopupHelper Instance
        {
            get
            {
                if (s_instance != null) return s_instance;
                s_instance = Resources.Load<PopupHelper>(s_loadPath);
                if (s_instance != null) return s_instance;
                s_instance = CreateInstance<PopupHelper>();
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.CreateAsset(s_instance, s_savePath);
#endif
                return s_instance;
            }
        }

        #endregion

        [SerializeField] public Dictionary<PopupDesignation, PopupData> Popups = new();

        UIPopup popupErrorNotEnoughMoney;

        public void Init()
        {
            popupErrorNotEnoughMoney = null;
        }

        public void HandleApplicationQuit()
        {
            popupErrorNotEnoughMoney = null;
        }

        public void ShowErrorPopup(PopupDesignation popupDesignation)
        {
            var popupData = Popups[popupDesignation];

            if (popupErrorNotEnoughMoney is not null)
                popupErrorNotEnoughMoney.Hide();

            popupErrorNotEnoughMoney = UIPopup
                .Get(popupData.PopupName)
                .SetTexts(popupData.PopupTextKeys[0]);
            popupErrorNotEnoughMoney.Show();
            popupErrorNotEnoughMoney.OnHideCallback.Event.AddListener(HandleHideCallback);

            void HandleHideCallback()
            {
                popupErrorNotEnoughMoney.OnHideCallback.Event.RemoveListener(HandleHideCallback);
                popupErrorNotEnoughMoney = null;
            }
        }
    }

    public class PopupData
    {
        [field: SerializeField, Required] public UIPopupLink PopupLink { get; private set; }
        [field: SerializeField] public List<string> PopupTextKeys { get; private set; }
        [field: SerializeField] public List<Sprite> PopupSprites { get; private set; }

        public string PopupName => PopupLink.prefabName;
    }

    public enum PopupDesignation
    {
        None = 0,

        ErrorNotEnoughMoney = 1,
    }
}