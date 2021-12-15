using System;
using BLUE.InventorySystem;
using BLUE.TriggerSystem;

namespace BLUE.ProjectX.Inventory
{
    public class Trigger_RangedWeaponInventoryUpdated : TriggerBase
    {
        #region Events

        protected override Action OnTriggerAttempt { get; set; }

        #endregion
        
        protected override void ActivateCustomActions()
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<ERangedWeaponItem, 
                ItemData_RangedWeapon, EInventory> rangedInventory);

            rangedInventory.OnItemDataUpdated += OnInventoryUpdated;
            
            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<ERangedWeaponItem, 
                ItemData_RangedWeapon, EInventory> rangedInventory);

            rangedInventory.OnItemDataUpdated -= OnInventoryUpdated;
            
            base.DeactivateCustomActions();
        }

        private void OnInventoryUpdated(ERangedWeaponItem itemType, 
            ItemData_RangedWeapon prevData, ItemData_RangedWeapon newData)
        {
            OnTriggerAttempt?.Invoke();
        }
    }
}