using System;
using BLUE.InventorySystem;
using BLUE.ProjectX.Ultimate;
using BLUE.TriggerSystem;

namespace BLUE.ProjectX.Inventory
{
    public class Trigger_UltimateInventoryUpdated : TriggerBase
    {
        #region Events

        protected override Action OnTriggerAttempt { get; set; }

        #endregion
        
        protected override void ActivateCustomActions()
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<EUltimate, 
                ItemData_Ultimate, EInventory> ultimateInventory);

            ultimateInventory.OnItemDataUpdated += OnInventoryUpdated;
            
            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<EUltimate, 
                ItemData_Ultimate, EInventory> ultimateInventory);

            ultimateInventory.OnItemDataUpdated -= OnInventoryUpdated;
            
            base.DeactivateCustomActions();
        }

        private void OnInventoryUpdated(EUltimate itemType, 
            ItemData_Ultimate prevData, ItemData_Ultimate newData)
        {
            OnTriggerAttempt?.Invoke();
        }
    }
}