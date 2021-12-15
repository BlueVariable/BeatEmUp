using BLUE.InventorySystem;
using BLUE.ProjectX.Inventory;

namespace BLUE.ProjectX.UI
{
    public class EquippedInventoryItemUIDataProvider_RangedWeapon : EquippedInventoryItemUIDataProviderBase
    {
        private InventoryItemUIDataProvider_RangedWeapon _rangedWeaponUIDataProvider =
            new InventoryItemUIDataProvider_RangedWeapon();
        public override InventoryItemUIData GetInventoryItemUIData()
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<ERangedWeaponItem, 
                ItemData_RangedWeapon, EInventory> rangedWeaponInventory);

            if (!((SavedInventory_RangedWeapon) rangedWeaponInventory).TryGetEquippedWeapon(
                out ERangedWeaponItem rangedWeaponItem))
            {
                return new InventoryItemUIData(string.Empty, string.Empty, null);
            }

            return _rangedWeaponUIDataProvider.GetItemUIData(rangedWeaponItem);
        }
    }
}
