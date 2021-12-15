using BLUE.ProjectX.Inventory;

namespace BLUE.ProjectX.UI
{
    public class UI_InventoryItem_RangedWeapon : UI_InventoryItemBase<ERangedWeaponItem>
    {
        public ERangedWeaponItem RangedWeaponItem { get; private set; } 
        
        private InventoryItemUIDataProvider_RangedWeapon _uiDataProvider = new InventoryItemUIDataProvider_RangedWeapon();

        protected override InventoryItemUIData GetItemUIData(ERangedWeaponItem rangedWeaponItem)
        {
            InventoryItemUIData uiData = _uiDataProvider.GetItemUIData(rangedWeaponItem);

            return uiData;
        }

        protected override void InitItemCustomActions(ERangedWeaponItem itemType, bool isEquipped)
        {
            RangedWeaponItem = itemType;

            base.InitItemCustomActions(itemType, isEquipped);
        }
    }
}