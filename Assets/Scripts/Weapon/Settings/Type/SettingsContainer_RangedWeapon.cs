using UnityEngine;

namespace BLUE.ProjectX.Weapon
{
    public class SettingsContainer_RangedWeapon : MonoBehaviour
    {
        #region Singleton

        private static SettingsContainer_RangedWeapon _instance;
        public static SettingsContainer_RangedWeapon Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SettingsContainer_RangedWeapon>();
                }

                return _instance;
            }
        }

        #endregion

        [SerializeField] private SettingsSO_RangedWeapon _rangedWeaponSettingsColl = null;
        public SettingsSO_RangedWeapon RangedWeaponSettingsColl => _rangedWeaponSettingsColl;
    }
}
