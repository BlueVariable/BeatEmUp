using UnityEngine;

namespace BLUE.ProjectX.Ultimate
{
    public class SettingsContainer_Ultimate : MonoBehaviour
    {
        #region Singleton

        private static SettingsContainer_Ultimate _instance;
        public static SettingsContainer_Ultimate Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SettingsContainer_Ultimate>();
                }

                return _instance;
            }
        }

        #endregion

        [SerializeField] private SettingsSO_Ultimate _ultimateSettingsColl = null;
        public SettingsSO_Ultimate UltimateSettingsColl => _ultimateSettingsColl;
    }
}