using BLUE.ActionSystem;
using BLUE.InventorySystem;
using BLUE.ProjectX.Inventory;
using BLUE.ProjectX.Ultimate;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class Action_UIInventoryItem_Ultimate_Equip : ActionBase
    {
        [SerializeField] private UI_InventoryItem_Ultimate _ultimateInventoryItem = null;
        
        public override void Execute()
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<EUltimate, 
                ItemData_Ultimate, EInventory> ultimateInventory);

            ((SavedInventory_Ultimate) ultimateInventory).TryGetEquippedUltimate(out EUltimate equippedItem);
            
            ultimateInventory.SetItemData(equippedItem, 
                new ItemData_Ultimate(false));
            
            ultimateInventory.SetItemData(_ultimateInventoryItem.UltimateItem, 
                new ItemData_Ultimate(true));
            
            ((SavedInventory_Ultimate) ultimateInventory).TrySave();
        }
    }
}