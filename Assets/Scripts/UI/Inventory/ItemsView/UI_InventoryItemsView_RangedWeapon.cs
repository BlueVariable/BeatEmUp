using BLUE.ProjectX.Inventory;

namespace BLUE.ProjectX.UI
{
    public class UI_InventoryItemsView_RangedWeapon : UI_InventoryItemsViewBase
    {
        protected override EInventory GetInventoryViewType()
        {
            return EInventory.RangedWeapon;
        }

        protected override void InitItems()
        {
            throw new System.NotImplementedException();
        }
    }
}