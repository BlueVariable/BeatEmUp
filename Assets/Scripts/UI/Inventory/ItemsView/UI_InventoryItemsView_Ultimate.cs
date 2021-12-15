using System.Collections.Generic;
using BLUE.InventorySystem;
using BLUE.ProjectX.Inventory;
using BLUE.ProjectX.Ultimate;

namespace BLUE.ProjectX.UI
{
    public class UI_InventoryItemsView_Ultimate : UI_InventoryItemsViewBase
    {
        protected override EInventory GetInventoryViewType()
        {
            return EInventory.Ultimate;
        }

        protected override void InitItems(UI_InventoryItemBase[] items)
        {
            InventoryController invController = Character.Character.Instance.GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<EUltimate, 
                ItemData_Ultimate, EInventory> ultimateInventory);

            List<InventoryBase<EUltimate, ItemData_Ultimate, EInventory>.Item> itemColl 
                = ultimateInventory.GetItemColl();
            
            for (int i = 0; i < itemColl.Count; i++)
            {
                items[i].InitItem(itemColl[i].ItemType, itemColl[i].ItemData.IsEquipped);
            }
        }
    }
}