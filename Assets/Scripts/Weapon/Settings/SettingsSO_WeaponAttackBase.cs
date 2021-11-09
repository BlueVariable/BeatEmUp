using System;
using System.Linq;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ProjectX.Weapon
{
    [Serializable]
    public abstract class Settings_WeaponAttackBase
    {
        [SerializeField] private EWeapon _weaponType = default;
        public EWeapon WeaponType => _weaponType;
    }
    
    [Serializable]
    public class Settings_WeaponAttack<T> : Settings_WeaponAttackBase
        where T : AttackDataBase
    {
        [SerializeField] private T[] _attackDataColl = default;
        public T[] AttackDataColl => _attackDataColl;
    }

    public abstract class SettingsSO_WeaponAttackBase : ScriptableObject
    {
    }
    
    public abstract class SettingsSO_WeaponAttackBase<T> : SettingsSO_WeaponAttackBase
        where T : AttackDataBase
    {
        [SerializeField] private Settings_WeaponAttack<T>[] _weaponAttackSettingsColl = null;

        public bool TryGetWeaponAttackSettings(EWeapon weaponType, out Settings_WeaponAttack<T> settings)
        {
            settings = _weaponAttackSettingsColl.FirstOrDefault(val 
                => val.WeaponType.Equals(weaponType));

            return settings != null;
        }
    }
}