using System;
using System.Linq;
using BLUE.ProjectX.Inventory;
using UnityEngine;

namespace BLUE.ProjectX.Weapon
{
    [Serializable]
    public class Settings_RangedWeapon
    {
        [SerializeField] private EWeapon _weaponType = default;
        public EWeapon WeaponType => _weaponType;

        [SerializeField] private ERangedWeaponItem _rangedWeaponItemType = default;
        public ERangedWeaponItem RangedWeaponItemType => _rangedWeaponItemType;

        [SerializeField] private Weapon _weaponPrefab = null;
        public Weapon WeaponPrefab => _weaponPrefab;
    }

    [CreateAssetMenu(fileName = "SettingsSO_RangedWeapon", menuName = "Settings/Create SettingsSO_RangedWeapon")]
    public class SettingsSO_RangedWeapon : ScriptableObject
    {
        [SerializeField] private Settings_RangedWeapon[] _rangedWeaponSettingsColl = null;

        public bool TryGetRangedWeaponSettings(EWeapon weaponType, out Settings_RangedWeapon settings)
        {
            settings = _rangedWeaponSettingsColl.FirstOrDefault(val 
                => val.WeaponType.Equals(weaponType));

            return settings != null;
        }
        
        public bool TryGetRangedWeaponSettings(ERangedWeaponItem rangedWeaponItemType, out Settings_RangedWeapon settings)
        {
            settings = _rangedWeaponSettingsColl.FirstOrDefault(val 
                => val.RangedWeaponItemType.Equals(rangedWeaponItemType));

            return settings != null;
        }
    }
}