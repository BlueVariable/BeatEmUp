using BLUE.InventorySystem;
using BLUE.ProjectX.Inventory;

namespace BLUE.ProjectX.UI
{
    public class EquippedInventoryItemUIDataProvider_MeleeWeapon : EquippedInventoryItemUIDataProviderBase
    {
        private InventoryItemUIDataProvider_MeleeWeapon _meleeWeaponUIDataProvider =
            new InventoryItemUIDataProvider_MeleeWeapon();
        public override InventoryItemUIData GetInventoryItemUIData()
        {
            InventoryController invController = Character.Character.Instance
                .GetComponentInChildren<InventoryController>();

            invController.TryGetInventory(out InventoryBase<EMeleeWeaponItem, 
                ItemData_MeleeWeapon, EInventory> meleeWeaponInventory);

            if (!((SavedInventory_MeleeWeapon) meleeWeaponInventory).TryGetEquippedWeapon(
                out EMeleeWeaponItem meleeWeaponItem))
            {
                return new InventoryItemUIData(string.Empty, string.Empty, null);
            }

            return _meleeWeaponUIDataProvider.GetItemUIData(meleeWeaponItem);
        }
    }
}