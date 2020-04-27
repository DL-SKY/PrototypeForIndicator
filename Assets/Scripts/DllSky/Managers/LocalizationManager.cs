using DllSky.Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

namespace DllSky.Managers
{
    [Serializable]
    public class LocalizationData
    {
        public List<LocalizationItem> strings = new List<LocalizationItem>();
    }

    [Serializable]
    public class LocalizationItem
    {
        public string id;

        public string rus;
        public string eng;
        public string deu;
    }

    public static class EventLocalizationConstants
    {
        public const string CHANGE_LANGUAGE = "CHANGE_LANGUAGE";                                //data = EnumLanguages 
        public const string APPLY_LANGUAGE_SETTINGS = "APPLY_LANGUAGE_SETTINGS";
    }

    public enum EnumLanguages
    {
        ENGLISH,
        RUSSIAN,        
        DEUTSCH,
    }

    public class LocalizationManager : Singleton<LocalizationManager>
    {
        #region Variables
        [SerializeField]
        private TextAsset json;
        [SerializeField]
        private LocalizationData data;

        [SerializeField]
        private EnumLanguages language = EnumLanguages.ENGLISH;

        private Dictionary<string, string> localization = new Dictionary<string, string>();
        #endregion

        #region Unity methods
        protected override void Awake()
        {
            base.Awake();

            Initialize();
        }

        private void OnEnable()
        {
            EventManager.AddEventListener(EventLocalizationConstants.CHANGE_LANGUAGE, OnChangeLanguageHandler);
        }

        private void OnDisable()
        {
            EventManager.RemoveEventListener(EventLocalizationConstants.CHANGE_LANGUAGE, OnChangeLanguageHandler);
        }
        #endregion

        #region Public methods
        public string GetString(string _key)
        {
            if (!localization.ContainsKey(_key) || string.IsNullOrEmpty(localization?[_key]))
                return _key;

            return localization[_key];
        }
        #endregion

        #region Private methods
        private void Initialize()
        {
            LoadLocalization();
        }

        private void OnChangeLanguageHandler(CustomEvent _event)
        {
            language = (EnumLanguages)_event.EventData;
            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
            localization.Clear();

            for (int i = 0; i < data.strings.Count; i++)
            {
                string key = data.strings[i].id;
                string value = "";

                switch (language)
                {
                    case EnumLanguages.RUSSIAN:
                        value = data.strings[i].rus;
                        break;
                    case EnumLanguages.ENGLISH:
                        value = data.strings[i].eng;
                        break;
                    case EnumLanguages.DEUTSCH:
                        value = data.strings[i].deu;
                        break;

                    default:
                        value = data.strings[i].eng;
                        break;
                }

                value = value.Replace("\\n", Environment.NewLine);
                localization.Add(key, value);
            }

            EventManager.DispatchEvent(EventLocalizationConstants.APPLY_LANGUAGE_SETTINGS);
        }

        private void LoadLocalization()
        {
            data = JsonUtility.FromJson<LocalizationData>(json.text);
        }
        #endregion

        #region Context Menu
        [ContextMenu("To JSON")]
        private void ToJson()
        {
            Debug.LogWarning(JsonUtility.ToJson(data, true));
        }
        #endregion
    }
}
