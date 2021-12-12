using BLUE.ProjectX.Inventory;

namespace BLUE.ProjectX.UI
{
    public class UI_InventoryItemsView_Ultimate : UI_InventoryItemsViewBase
    {
        protected override EInventory GetInventoryViewType()
        {
            return EInventory.Ultimate;
        }

        protected override void InitItems()
        {
            throw new System.NotImplementedException();
        }
    }
}