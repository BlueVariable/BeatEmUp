using System;
using System.Collections.Generic;
using System.Linq;
using BLUE.ProjectX.Inventory;
using BLUE.TriggerSystem;
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

            [SerializeField] private UI_Button_Category _targetButton = null;
            public UI_Button_Category TargetButton => _targetButton;
        }

        [SerializeField] private UI_Selector_Simple _simpleSelector = null;

        [SerializeField] private InventoryCategoryButtonInfo[] _invCategoryButtonInfoColl = null;

        [SerializeField] private TriggerBase[] _categoryButtonsUpdateRequestors = null;

        public EInventory SelectedCategory { get; private set; } = EInventory.MeleeWeapon;
        
        private Dictionary<EInventory, EquippedInventoryItemUIDataProviderBase> _inventoryDataProviderMapping;
        private Dictionary<EInventory, EquippedInventoryItemUIDataProviderBase> _InventoryDataProviderMapping
        {
            get
            {
                if (_inventoryDataProviderMapping == null)
                {
                    _inventoryDataProviderMapping = new Dictionary<EInventory, EquippedInventoryItemUIDataProviderBase>()
                    {
                        { EInventory.Ultimate, new EquippedInventoryItemUIDataProvider_Ultimate() },
                        { EInventory.MeleeWeapon, new EquippedInventoryItemUIDataProvider_MeleeWeapon() },
                        { EInventory.RangedWeapon, new EquippedInventoryItemUIDataProvider_RangedWeapon() }
                    };
                }

                return _inventoryDataProviderMapping;
            }
        }

        #region Events

        public Action<EInventory> OnCategorySelected { get; set; }

        #endregion
        
        protected override void ActivatingCustomActions(Action onCompleted = null)
        {
            RegisterToCategoryButtons();

            RegisterToCategoryButtonsUpdateRequestors();

            InitCategoryButtons();

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
            UnregisterFromCategoryButtons();
            
            base.DeactivatingCustomActions(onCompleted);
        }

        private void RegisterToCategoryButtons()
        {
            Array.ForEach(_invCategoryButtonInfoColl, val 
                => val.TargetButton.OnClick += OnButtonClick);
        }

        private void UnregisterFromCategoryButtons()
        {
            Array.ForEach(_invCategoryButtonInfoColl, val 
                => val.TargetButton.OnClick -= OnButtonClick);
        }

        private void RegisterToCategoryButtonsUpdateRequestors()
        {
            Array.ForEach(_categoryButtonsUpdateRequestors, val
                => val.OnTriggered += OnCategoryButtonsUpdateRequested);
        }
        
        private void UnregisterFromCategoryButtonsUpdateRequestors()
        {
            Array.ForEach(_categoryButtonsUpdateRequestors, val
                => val.OnTriggered -= OnCategoryButtonsUpdateRequested);
        }

        private void InitCategoryButtons()
        {
            foreach (InventoryCategoryButtonInfo buttonInfo in _invCategoryButtonInfoColl)
            {
                EquippedInventoryItemUIDataProviderBase uiDataProvider = _InventoryDataProviderMapping[buttonInfo.InventoryType];

                InventoryItemUIData uiData = uiDataProvider.GetInventoryItemUIData();

                buttonInfo.TargetButton.SetSelectedItemIcon(uiData.ItemIcon);
            }
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
        
        private void OnCategoryButtonsUpdateRequested(TriggerBase trigger)
        {
            InitCategoryButtons();
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