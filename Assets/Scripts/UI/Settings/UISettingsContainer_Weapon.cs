using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class UISettingsContainer_Weapon : MonoBehaviour
    {
        #region Singleton

        private static UISettingsContainer_Weapon _instance;
        public static UISettingsContainer_Weapon Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UISettingsContainer_Weapon>();
                }

                return _instance;
            }
        }

        #endregion

        [SerializeField] private UISettingsSO_Weapon _weaponUISettingsSO = null;
        public UISettingsSO_Weapon WeaponUISettingsSO => _weaponUISettingsSO;
    }
}