using BLUE.ProjectX.Ultimate;

namespace BLUE.ProjectX.UI
{
    public class UI_InventoryItem_Ultimate : UI_InventoryItemBase<EUltimate>
    {
        public EUltimate UltimateItem { get; private set; } 
        
        private InventoryItemUIDataProvider_Ultimate _uiDataProvider = new InventoryItemUIDataProvider_Ultimate();

        protected override InventoryItemUIData GetItemUIData(EUltimate ultimateItem)
        {
            InventoryItemUIData uiData = _uiDataProvider.GetItemUIData(ultimateItem);

            return uiData;
        }

        protected override void InitItemCustomActions(EUltimate itemType, bool isEquipped)
        {
            UltimateItem = itemType;

            base.InitItemCustomActions(itemType, isEquipped);
        }
    }
}