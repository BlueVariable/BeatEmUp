using System.Collections.Generic;
using BLUE.EquipmentSystem;
using BLUE.ProjectX.Inventory;
using BLUE.ProjectX.Ultimate;
using UnityEngine;

namespace BLUE.ProjectX.Equipment
{
    public class InitialEquipper_UltimateInventoryBased : InitialEquipperBase
    {
        [SerializeField] private SavedInventory_Ultimate _ultimateInventory = null;

        [SerializeField] private Transform _equipBone = null;

        private List<EquipmentSystem.Equipment> _equippedEquipments = new List<EquipmentSystem.Equipment>();
        protected override List<EquipmentSystem.Equipment> _InitialEquippedEquipments => _equippedEquipments;

        protected override void EquipInitialEquipments()
        {
            if (!_ultimateInventory.TryGetEquippedUltimate(out EUltimate ultimateType))
            {
                return;
            }

            SettingsContainer_Ultimate.Instance.UltimateSettingsColl
                .TryGetUltimateSettings(ultimateType, out Settings_Ultimate settings);

            UltimateBase ultimate = Instantiate(settings.UltimatePrefab, _equipBone);

            EquipmentSystem.Equipment ultimateEquipment = ultimate.GetComponent<EquipmentSystem.Equipment>();

            _equippedEquipments.Add(ultimateEquipment);
        }
    }
}