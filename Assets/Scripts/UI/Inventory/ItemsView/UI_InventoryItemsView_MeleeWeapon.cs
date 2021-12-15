using System.Collections.Generic;
using BLUE.InventorySystem;
using BLUE.ProjectX.Inventory;
using BLUE.UISystem;

namespace BLUE.ProjectX.UI
{
    public class UI_InventoryItemsView_MeleeWeapon : UI_InventoryItemsViewBase
    {
        protected override EInventory GetInventoryViewType()
        {
            return EInventory.MeleeWeapon;
        }

        protected override void InitItems(UI_InventoryItemBase[] items, UI_Selector_Simple simpleSelector)
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<EMeleeWeaponItem, 
                ItemData_MeleeWeapon, EInventory> meleeWeaponInventory);

            List<InventoryBase<EMeleeWeaponItem, ItemData_MeleeWeapon, EInventory>.Item> itemColl 
                = meleeWeaponInventory.GetItemColl();
            
            for (int i = 0; i < itemColl.Count; i++)
            {
                items[i].InitItem(itemColl[i].ItemType);
                
                if (itemColl[i].ItemData.IsEquipped)
                {
                    simpleSelector.TrySelectSelectable(items[i].Selectable);
                }
            }
        }
    }
}