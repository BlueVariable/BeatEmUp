using System;
using System.Linq;
using BLUE.ProjectX.Inventory;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class UI_InventoryCategory : UIBase
    {
        [Serializable]
        private class InventoryCategoryButtonInfo
        {
            [SerializeField] private EInventory _inventoryType = EInventory.None;
            public EInventory InventoryType => _inventoryType;

            [SerializeField] private ButtonBase _targetButton = null;
            public ButtonBase TargetButton => _targetButton;
        }

        [SerializeField] private UI_Selector_Simple _simpleSelector = null;

        [SerializeField] private InventoryCategoryButtonInfo[] _invCategoryButtonInfoColl = null;

        public EInventory SelectedCategory { get; private set; } = EInventory.MeleeWeapon;

        #region Events

        public Action<EInventory> OnCategorySelected { get; set; }

        #endregion
        
        protected override void ActivatingCustomActions(Action onCompleted = null)
        {
            Array.ForEach(_invCategoryButtonInfoColl, val 
                => val.TargetButton.OnClick += OnButtonClick);
            
            base.ActivatingCustomActions(onCompleted);
        }

        protected override void ActivatedCustomActions()
        {
            ButtonBase button = GetButtonBy(SelectedCategory);
            
            UI_SelectableBase selectable = button.GetComponent<UI_SelectableBase>();
            
            _simpleSelector.TrySelectSelectable(selectable);
            
            base.ActivatedCustomActions();
        }

        protected override void DeactivatingCustomActions(Action onCompleted = null)
        {
            Array.ForEach(_invCategoryButtonInfoColl, val 
                => val.TargetButton.OnClick -= OnButtonClick);
            
            base.DeactivatingCustomActions(onCompleted);
        }

        private void OnButtonClick(ButtonBase button)
        {
            EInventory inventoryCategory = GetCategoryBy(button);

            UI_SelectableBase selectable = button.GetComponent<UI_SelectableBase>();

            if (selectable != null)
            {
                _simpleSelector.TrySelectSelectable(selectable);
            }
            
            TrySelectCategory(inventoryCategory);
        }
        
        private ButtonBase GetButtonBy(EInventory inventoryType)
        {
            InventoryCategoryButtonInfo info = _invCategoryButtonInfoColl
                .FirstOrDefault(val => val.InventoryType == inventoryType);

            return info?.TargetButton;
        }

        private EInventory GetCategoryBy(ButtonBase button)
        {
            InventoryCategoryButtonInfo info = _invCategoryButtonInfoColl
                .FirstOrDefault(val => val.TargetButton == button);

            if (info != null)
            {
                return info.InventoryType;
            }

            return default;
        }

        private bool TrySelectCategory(EInventory inventoryType)
        {
            if (inventoryType == SelectedCategory)
            {
                return false;
            }

            if (inventoryType != EInventory.None)
            {
                SelectedCategory = inventoryType;
                
                OnCategorySelected?.Invoke(inventoryType);

                return true;
            }

            return false;
        }
    }
}