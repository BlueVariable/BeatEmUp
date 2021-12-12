using BLUE.ProjectX.Inventory;

namespace BLUE.ProjectX.UI
{
    public class UI_InventoryItemsView_MeleeWeapon : UI_InventoryItemsViewBase
    {
        protected override EInventory GetInventoryViewType()
        {
            return EInventory.MeleeWeapon;
        }

        protected override void InitItems()
        {
            throw new System.NotImplementedException();
        }
    }
}