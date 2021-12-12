using System;
using BLUE.ProjectX.Inventory;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public abstract class UI_InventoryItemsViewBase : UIBase
    {
        [SerializeField] private UI_InventoryCategory _inventoryCategory = null;

        protected abstract EInventory GetInventoryViewType();

        protected abstract void InitItems();
        
        protected override void AwakeCustomActions()
        {
            if (_inventoryCategory.SelectedCategory == GetInventoryViewType())
            {
                TryActivate();
            }

            RegisterToInventoryCategory();
            
            base.AwakeCustomActions();
        }

        protected override void ActivatingCustomActions(Action onCompleted = null)
        {
            InitItems();
            
            base.ActivatingCustomActions(onCompleted);
        }

        protected override void DestroyCustomActions()
        {
            UnregisterFromInventoryCategory();
            
            base.DestroyCustomActions();
        }

        private void RegisterToInventoryCategory()
        {
            _inventoryCategory.OnCategorySelected += OnCategorySelected;
        }

        private void UnregisterFromInventoryCategory()
        {
            _inventoryCategory.OnCategorySelected -= OnCategorySelected;
        }

        private void OnCategorySelected(EInventory inventoryType)
        {
            if (_inventoryCategory.SelectedCategory == GetInventoryViewType())
            {
                TryActivate();
            }
            else
            {
                TryDeactivate();
            }
        }
    }
}