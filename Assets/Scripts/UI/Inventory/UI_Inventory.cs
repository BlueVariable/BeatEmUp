using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class UI_Inventory : UIBase
    {
        [SerializeField] private UI_InventoryCategory _inventoryCategory = null;
        
        [SerializeField] private UI_InventoryItemInfoContainer _itemInfoContainer = null;

        protected override void ActivatedCustomActions()
        {
            _inventoryCategory.TryActivate();
            
            _itemInfoContainer.TryActivate();
            
            base.ActivatedCustomActions();
        }

        protected override void DeactivatedCustomActions()
        {
            _inventoryCategory.TryDeactivate();
            
            _itemInfoContainer.TryDeactivate();
            
            base.DeactivatedCustomActions();
        }
    }
}