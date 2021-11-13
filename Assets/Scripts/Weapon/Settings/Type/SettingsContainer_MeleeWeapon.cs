using UnityEngine;

namespace BLUE.ProjectX.Weapon
{
    public class SettingsContainer_MeleeWeapon : MonoBehaviour
    {
        #region Singleton

        private static SettingsContainer_MeleeWeapon _instance;
        public static SettingsContainer_MeleeWeapon Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SettingsContainer_MeleeWeapon>();
                }

                return _instance;
            }
        }

        #endregion

        [SerializeField] private SettingsSO_MeleeWeapon _meleeWeaponSettingsColl = null;
        public SettingsSO_MeleeWeapon MeleeWeaponSettingsColl => _meleeWeaponSettingsColl;
    }
}