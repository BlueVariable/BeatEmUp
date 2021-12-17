using BLUE.InventorySystem;
using BLUE.ProjectX.Inventory;
using BLUE.ProjectX.Ultimate;

namespace BLUE.ProjectX.UI
{
    public class EquippedInventoryItemUIDataProvider_Ultimate : EquippedInventoryItemUIDataProviderBase
    {
        private InventoryItemUIDataProvider_Ultimate _ultimateUIDataProvider =
            new InventoryItemUIDataProvider_Ultimate();
        public override InventoryItemUIData GetInventoryItemUIData()
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<EUltimate, 
                ItemData_Ultimate, EInventory> ultimateInventory);
            
            if (!((SavedInventory_Ultimate) ultimateInventory).TryGetEquippedUltimate(
                out EUltimate ultimateItem))
            {
                return new InventoryItemUIData(string.Empty, string.Empty, 
                    string.Empty, null);
            }

            return _ultimateUIDataProvider.GetItemUIData(ultimateItem);
        }
    }
}

