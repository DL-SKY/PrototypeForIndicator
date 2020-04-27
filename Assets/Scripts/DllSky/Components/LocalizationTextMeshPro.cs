using DllSky.Managers;
using TMPro;
using UnityEngine;

namespace DllSky.Components
{
    [AddComponentMenu("UI/Localization/LocalizationTextMeshPro")]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizationTextMeshPro : MonoBehaviour
    {
        #region Variables
        public string key = "";

        private TextMeshProUGUI text;
        private string localizationString = "";
        #endregion

        #region Unity methods
        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            EventManager.AddEventListener(EventLocalizationConstants.APPLY_LANGUAGE_SETTINGS, OnApplyLanguageHandler);

            ApplyLocalization();
        }

        private void OnDisable()
        {
            EventManager.RemoveEventListener(EventLocalizationConstants.APPLY_LANGUAGE_SETTINGS, OnApplyLanguageHandler);
        }
        #endregion

        #region Private methods
        private void OnApplyLanguageHandler(CustomEvent _event)
        {
            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
            if (!text)
                return;

            localizationString = LocalizationManager.Instance.GetString(key);
            text.text = localizationString.ToString();
        }
        #endregion
    }
}
