using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSample_Resources
{
    [CreateAssetMenu(fileName = "CurrencyHelper", menuName = "Scriptable Objects/CurrencyHelper")]
    public class CurrencyHelper : SerializedScriptableObject
    {
        #region Singleton
        private static string s_assetName => nameof(CurrencyHelper);
        private static string s_loadPath => $"ScriptableObjects/Helpers/{s_assetName}";
        private static string s_savePath => $"Assets/Resources/{s_loadPath}.asset";

        private static CurrencyHelper s_instance;

        public static CurrencyHelper Instance
        {
            get
            {
                if (s_instance != null) return s_instance;
                s_instance = Resources.Load<CurrencyHelper>(s_loadPath);
                if (s_instance != null) return s_instance;
                s_instance = CreateInstance<CurrencyHelper>();
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.CreateAsset(s_instance, s_savePath);
#endif
                return s_instance;
            }
        }
        #endregion

        [field: SerializeField] public Dictionary<CurrencyType, CurrencyInfo> CurrencyInfos { get; private set; }
    }
}
