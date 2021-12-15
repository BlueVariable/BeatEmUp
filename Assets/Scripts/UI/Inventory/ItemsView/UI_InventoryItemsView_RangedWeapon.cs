using System.Collections.Generic;
using BLUE.InventorySystem;
using BLUE.ProjectX.Inventory;
using BLUE.UISystem;

namespace BLUE.ProjectX.UI
{
    public class UI_InventoryItemsView_RangedWeapon : UI_InventoryItemsViewBase
    {
        protected override EInventory GetInventoryViewType()
        {
            return EInventory.RangedWeapon;
        }

        protected override void InitItems(UI_InventoryItemBase[] items, UI_Selector_Simple simpleSelector)
        {
            InventoryController invController = Character.Character.Instance.GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<ERangedWeaponItem, 
                ItemData_RangedWeapon, EInventory> rangedWeaponInventory);

            List<InventoryBase<ERangedWeaponItem, ItemData_RangedWeapon, EInventory>.Item> itemColl 
                = rangedWeaponInventory.GetItemColl();
            
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