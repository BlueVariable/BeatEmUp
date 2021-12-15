using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class UISettingsContainer_Ultimate : MonoBehaviour
    {
        #region Singleton

        private static UISettingsContainer_Ultimate _instance;
        public static UISettingsContainer_Ultimate Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UISettingsContainer_Ultimate>();
                }

                return _instance;
            }
        }

        #endregion

        [SerializeField] private UISettingsSO_Ultimate _ultimateUISettingsSO = null;
        public UISettingsSO_Ultimate UltimateUISettingsSO => _ultimateUISettingsSO;
    }
}