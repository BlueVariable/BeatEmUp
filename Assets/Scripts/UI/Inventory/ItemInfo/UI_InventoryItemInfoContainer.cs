using System;
using System.Collections.Generic;
using BLUE.ProjectX.Inventory;
using BLUE.TriggerSystem;
using BLUE.UISystem;
using TMPro;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class UI_InventoryItemInfoContainer : UIBase
    {
        [SerializeField] private TextMeshProUGUI _displayName = null;

        [SerializeField] private TextMeshProUGUI _description = null;

        [SerializeField] private UI_InventoryCategory _inventoryCategory = null;

        [SerializeField] private TriggerBase[] _refreshRequestors = null;

        private EInventory _activeCategory = EInventory.None;

        private Dictionary<EInventory, EquippedInventoryItemUIDataProviderBase> _inventoryDataProviderMapping;

        private Dictionary<EInventory, EquippedInventoryItemUIDataProviderBase> _InventoryDataProviderMapping
        {
            get
            {
                if (_inventoryDataProviderMapping == null)
                {
                    _inventoryDataProviderMapping =
                        new Dictionary<EInventory, EquippedInventoryItemUIDataProviderBase>()
                        {
                            {EInventory.Ultimate, new EquippedInventoryItemUIDataProvider_Ultimate()},
                            {EInventory.MeleeWeapon, new EquippedInventoryItemUIDataProvider_MeleeWeapon()},
                            {EInventory.RangedWeapon, new EquippedInventoryItemUIDataProvider_RangedWeapon()}
                        };
                }

                return _inventoryDataProviderMapping;
            }
        }

        protected override void ActivatedCustomActions()
        {
            _activeCategory = _inventoryCategory.SelectedCategory;

            RegisterToInventoryCategory();

            RegisterToRefreshRequestors();

            base.ActivatedCustomActions();
        }

        protected override void DeactivatedCustomActions()
        {
            UnregisterFromInventoryCategory();

            UnregisterFromRefreshRequestors();

            base.DeactivatedCustomActions();
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
            _activeCategory = inventoryType;

            InitView();
        }

        private void RegisterToRefreshRequestors()
        {
            Array.ForEach(_refreshRequestors, val => val.OnTriggered += OnRefreshRequested);
        }

        private void UnregisterFromRefreshRequestors()
        {
            Array.ForEach(_refreshRequestors, val => val.OnTriggered -= OnRefreshRequested);
        }

        private void OnRefreshRequested(TriggerBase trigger)
        {
            InitView();
        }

        private void InitView()
        {
            InventoryItemUIData data = _InventoryDataProviderMapping[_activeCategory].GetInventoryItemUIData();

            _displayName.text = data.DisplayName;

            _description.text = data.Description;
        }
    }
}