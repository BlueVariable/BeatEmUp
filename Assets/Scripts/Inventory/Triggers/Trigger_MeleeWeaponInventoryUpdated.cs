using System;
using BLUE.InventorySystem;
using BLUE.TriggerSystem;

namespace BLUE.ProjectX.Inventory
{
    public class Trigger_MeleeWeaponInventoryUpdated : TriggerBase
    {
        #region Events

        protected override Action OnTriggerAttempt { get; set; }

        #endregion
        
        protected override void ActivateCustomActions()
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<EMeleeWeaponItem, 
                ItemData_MeleeWeapon, EInventory> meleeWeaponInventory);

            meleeWeaponInventory.OnItemDataUpdated += OnInventoryUpdated;
            
            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<EMeleeWeaponItem, 
                ItemData_MeleeWeapon, EInventory> meleeWeaponInventory);

            meleeWeaponInventory.OnItemDataUpdated -= OnInventoryUpdated;
            
            base.DeactivateCustomActions();
        }

        private void OnInventoryUpdated(EMeleeWeaponItem itemType, 
            ItemData_MeleeWeapon prevData, ItemData_MeleeWeapon newData)
        {
            OnTriggerAttempt?.Invoke();
        }
    }
}
