using BLUE.ActionSystem;
using BLUE.InventorySystem;
using BLUE.ProjectX.Inventory;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class Action_UIInventoryItem_Melee_Equip : ActionBase
    {
        [SerializeField] private UI_InventoryItem_MeleeWeapon _meleeWeaponInventoryItem = null;
        
        public override void Execute()
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<EMeleeWeaponItem, 
                ItemData_MeleeWeapon, EInventory> meleeWeaponInventory);

            ((SavedInventory_MeleeWeapon) meleeWeaponInventory).TryGetEquippedWeapon(out EMeleeWeaponItem equippedItem);
            
            meleeWeaponInventory.SetItemData(equippedItem, 
                new ItemData_MeleeWeapon(false));
            
            meleeWeaponInventory.SetItemData(_meleeWeaponInventoryItem.MeleeWeaponItem, 
                new ItemData_MeleeWeapon(true));

            ((SavedInventory_MeleeWeapon) meleeWeaponInventory).TrySave();
        }
    }
}