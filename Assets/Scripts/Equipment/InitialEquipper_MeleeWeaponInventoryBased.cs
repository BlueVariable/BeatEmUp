using System.Collections.Generic;
using BLUE.EquipmentSystem;
using BLUE.ProjectX.Inventory;
using BLUE.ProjectX.Weapon;
using UnityEngine;

namespace BLUE.ProjectX.Equipment
{
    public class InitialEquipper_MeleeWeaponInventoryBased : InitialEquipperBase
    {
        [SerializeField] private SavedInventory_MeleeWeapon _meleeWeaponInventory = null;

        [SerializeField] private Transform _equipBone = null;

        private List<EquipmentSystem.Equipment> _equippedEquipments = new List<EquipmentSystem.Equipment>();
        
        protected override List<EquipmentSystem.Equipment> _InitialEquippedEquipments => _equippedEquipments;

        protected override void EquipInitialEquipments()
        {
            if (!_meleeWeaponInventory.TryGetEquippedWeapon(out EMeleeWeaponItem itemType))
            {
                return;
            }

            SettingsContainer_MeleeWeapon.Instance.MeleeWeaponSettingsColl
                .TryGetMeleeWeaponSettings(itemType, out Settings_MeleeWeapon settings);

            Weapon.Weapon meleeWeapon = Instantiate(settings.WeaponPrefab, _equipBone);

            EquipmentSystem.Equipment weaponEquipment = meleeWeapon.GetComponent<EquipmentSystem.Equipment>();

            _equippedEquipments.Add(weaponEquipment);
        }
    }
}