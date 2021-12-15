using System;
using BLUE.ProjectX.Inventory;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public abstract class UI_InventoryItemsViewBase : UIBase
    {
        [SerializeField] protected UI_Selector_Simple _simpleSelector = null;
        
        [SerializeField] private UI_InventoryCategory _inventoryCategory = null;

        [SerializeField] private UI_InventoryItemBase[] _inventoryItems = null;

        protected abstract EInventory GetInventoryViewType();

        protected abstract void InitItems(UI_InventoryItemBase[] items, UI_Selector_Simple simpleSelector);
        
        protected override void AwakeCustomActions()
        {
            if (_inventoryCategory.SelectedCategory == GetInventoryViewType())
            {
                TryActivate();
            }

            RegisterToInventoryCategory();

            RegisterToItemButtons();
            
            base.AwakeCustomActions();
        }

        protected override void ActivatingCustomActions(Action onCompleted = null)
        {
            Array.ForEach(_inventoryItems, val => val.TryActivate());

            _simpleSelector.TryActivate();

            InitItems(_inventoryItems, _simpleSelector);
            
            base.ActivatingCustomActions(onCompleted);
        }

        protected override void DeactivatingCustomActions(Action onCompleted = null)
        {
            Array.ForEach(_inventoryItems, val => val.TryDeactivate());
            
            _simpleSelector.TryDeactivate();
            
            base.DeactivatingCustomActions(onCompleted);
        }

        protected override void DestroyCustomActions()
        {
            UnregisterFromInventoryCategory();

            UnregisterFromItemButtons();
            
            base.DestroyCustomActions();
        }

        private void RegisterToItemButtons()
        {
            foreach (UI_InventoryItemBase inventoryItem in _inventoryItems)
            {
                inventoryItem.Button.OnClick += OnInventoryItemClicked;
            }
        }
        
        private void UnregisterFromItemButtons()
        {
            foreach (UI_InventoryItemBase inventoryItem in _inventoryItems)
            {
                inventoryItem.Button.OnClick -= OnInventoryItemClicked;
            }
        }

        private void OnInventoryItemClicked(ButtonBase button)
        {
            _simpleSelector.TrySelectSelectable(button.GetComponent<UI_SelectableBase>());
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