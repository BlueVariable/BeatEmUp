using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class UI_Inventory : UIBase
    {
        [SerializeField] private UI_InventoryCategory _inventoryCategory = null;
        
        [SerializeField] private UI_InventoryItemInfo _itemInfoContainer = null;
        
        [SerializeField] private UI_EquippedItemDisplayer _equippedItemDisplayer = null;
        
        [SerializeField] private ButtonBase _backButton = null;

        protected override void ActivatedCustomActions()
        {
            _inventoryCategory.TryActivate();
            
            _itemInfoContainer.TryActivate();

            _equippedItemDisplayer.TryActivate();

            _backButton.TryActivate();
            
            base.ActivatedCustomActions();
        }

        protected override void DeactivatedCustomActions()
        {
            _inventoryCategory.TryDeactivate();
            
            _itemInfoContainer.TryDeactivate();

            _equippedItemDisplayer.TryDeactivate();

            _backButton.TryDeactivate();
            
            base.DeactivatedCustomActions();
        }
    }
}