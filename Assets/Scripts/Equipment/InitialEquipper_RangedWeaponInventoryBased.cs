using System.Collections.Generic;
using BLUE.EquipmentSystem;
using BLUE.ProjectX.Inventory;
using BLUE.ProjectX.Weapon;
using UnityEngine;

namespace BLUE.ProjectX.Equipment
{
    public class InitialEquipper_RangedWeaponInventoryBased : InitialEquipperBase
    {
        [SerializeField] private SavedInventory_RangedWeapon _rangedWeaponInventory = null;

        [SerializeField] private Transform _equipBone = null;

        private List<EquipmentSystem.Equipment> _equippedEquipments = new List<EquipmentSystem.Equipment>();
        
        protected override List<EquipmentSystem.Equipment> _InitialEquippedEquipments => _equippedEquipments;

        protected override void EquipInitialEquipments()
        {
            if (!_rangedWeaponInventory.TryGetEquippedWeapon(out ERangedWeaponItem itemType))
            {
                return;
            }

            SettingsContainer_RangedWeapon.Instance.RangedWeaponSettingsColl
                .TryGetRangedWeaponSettings(itemType, out Settings_RangedWeapon settings);

            Weapon.Weapon rangedWeapon = Instantiate(settings.WeaponPrefab, _equipBone);
            
            EquipmentSystem.Equipment weaponEquipment = rangedWeapon.GetComponent<EquipmentSystem.Equipment>();
            
            _equippedEquipments.Add(weaponEquipment);
        }
    }
}