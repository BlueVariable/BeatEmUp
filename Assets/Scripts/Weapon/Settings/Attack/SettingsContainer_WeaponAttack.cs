using BLUE.Common;
using UnityEngine;

namespace BLUE.ProjectX.Weapon
{
    public class SettingsContainer_WeaponAttack : MonoBehaviour
    {
        #region Singleton

        private static SettingsContainer_WeaponAttack _instance;
        public static SettingsContainer_WeaponAttack Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SettingsContainer_WeaponAttack>();
                }

                return _instance;
            }
        }

        #endregion

        [SerializeField] private SettingsSO_WeaponAttackBase[] _weaponAttackSettingsSOs = null;

        public bool GetAttackSettings<T>(EWeapon weaponType, out Settings_WeaponAttack<T> attackSettings)
            where T : AttackDataBase
        {
            attackSettings = default;
            
            foreach (SettingsSO_WeaponAttackBase candidateSettingsSO in _weaponAttackSettingsSOs)
            {
                if (candidateSettingsSO is SettingsSO_WeaponAttackBase<T> foundSO)
                {
                    foundSO.TryGetWeaponAttackSettings(weaponType, out attackSettings);

                    if (attackSettings != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}