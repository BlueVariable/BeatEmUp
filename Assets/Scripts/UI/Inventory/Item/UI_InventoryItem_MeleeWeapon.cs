using BLUE.ProjectX.Inventory;

namespace BLUE.ProjectX.UI
{
    public class UI_InventoryItem_MeleeWeapon : UI_InventoryItemBase<EMeleeWeaponItem>
    {
        public EMeleeWeaponItem MeleeWeaponItem { get; private set; } 
        
        private InventoryItemUIDataProvider_MeleeWeapon _uiDataProvider = new InventoryItemUIDataProvider_MeleeWeapon();

        protected override InventoryItemUIData GetItemUIData(EMeleeWeaponItem meleeWeaponItem)
        {
            InventoryItemUIData uiData = _uiDataProvider.GetItemUIData(meleeWeaponItem);

            return uiData;
        }

        protected override void InitItemCustomActions(EMeleeWeaponItem itemType)
        {
            MeleeWeaponItem = itemType;

            base.InitItemCustomActions(itemType);
        }
    }
}