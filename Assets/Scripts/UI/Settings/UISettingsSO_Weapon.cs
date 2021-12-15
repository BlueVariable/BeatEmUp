using System;
using System.Linq;
using BLUE.ProjectX.Weapon;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    [Serializable]
    public class UISettings_Weapon
    {
        [SerializeField] private EWeapon _weaponType = default;
        public EWeapon WeaponType => _weaponType;
        
        [SerializeField] private string _displayName = null;
        public string DisplayName => _displayName;

        [SerializeField] private string _description = null;
        public string Description => _description;
        
        [SerializeField] private Sprite _icon = null;
        public Sprite Icon => _icon;
    }

    [CreateAssetMenu(fileName = "UISettingsSO_Weapon", menuName = "BLUE/Settings/UI/Weapon/Create UISettingsSO_Weapon")]
    public class UISettingsSO_Weapon : ScriptableObject
    {
        [SerializeField] private UISettings_Weapon[] _weaponUISettingsColl = null;
        
        public bool TryGetWeaponUISettings(EWeapon weaponType, out UISettings_Weapon settings)
        {
            settings = _weaponUISettingsColl.FirstOrDefault(val 
                => val.WeaponType.Equals(weaponType));

            return settings != null;
        }
    }
}