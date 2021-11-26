using System;
using System.Linq;
using BLUE.ProjectX.Inventory;
using UnityEngine;

namespace BLUE.ProjectX.Weapon
{
    [Serializable]
    public class Settings_MeleeWeapon
    {
        [SerializeField] private EWeapon _weaponType = default;
        public EWeapon WeaponType => _weaponType;

        [SerializeField] private EMeleeWeaponItem _meleeWeaponItemType = default;
        public EMeleeWeaponItem MeleeWeaponItemType => _meleeWeaponItemType;

        [SerializeField] private Weapon _weaponPrefab = null;
        public Weapon WeaponPrefab => _weaponPrefab;
    }

    [CreateAssetMenu(fileName = "SettingsSO_MeleeWeapon", menuName = "BLUE/Settings/Weapon/Create SettingsSO_MeleeWeapon")]
    public class SettingsSO_MeleeWeapon : ScriptableObject
    {
        [SerializeField] private Settings_MeleeWeapon[] _meleeWeaponSettingsColl = null;

        public bool TryGetMeleeWeaponSettings(EWeapon weaponType, out Settings_MeleeWeapon settings)
        {
            settings = _meleeWeaponSettingsColl.FirstOrDefault(val 
                => val.WeaponType.Equals(weaponType));

            return settings != null;
        }
        
        public bool TryGetMeleeWeaponSettings(EMeleeWeaponItem meleeWeaponItemType, out Settings_MeleeWeapon settings)
        {
            settings = _meleeWeaponSettingsColl.FirstOrDefault(val 
                => val.MeleeWeaponItemType.Equals(meleeWeaponItemType));

            return settings != null;
        }
    }
}