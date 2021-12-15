using BLUE.ActionSystem;
using BLUE.InventorySystem;
using BLUE.ProjectX.Inventory;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class Action_UIInventoryItem_Ranged_Equip : ActionBase
    {
        [SerializeField] private UI_InventoryItem_RangedWeapon _rangedWeaponInventoryItem = null;
        
        public override void Execute()
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<ERangedWeaponItem, 
                ItemData_RangedWeapon, EInventory> rangedWeaponInventory);

            ((SavedInventory_RangedWeapon) rangedWeaponInventory).TryGetEquippedWeapon(out ERangedWeaponItem equippedItem);
            
            rangedWeaponInventory.SetItemData(equippedItem, 
                new ItemData_RangedWeapon(false));
            
            rangedWeaponInventory.SetItemData(_rangedWeaponInventoryItem.RangedWeaponItem, 
                new ItemData_RangedWeapon(true));
            
            ((SavedInventory_RangedWeapon) rangedWeaponInventory).TrySave();
        }
    }
}