using System;
using System.Collections.Generic;
using BLUE.ProjectX.Inventory;
using BLUE.TriggerSystem;
using BLUE.UISystem;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class UI_EquippedItemDisplayer : UIBase
    {
        [SerializeField] private UI_InventoryCategory _inventoryCategory = null;
        
        [SerializeField] private TriggerBase[] _refreshRequestors = null;

        [SerializeField] private Transform _displayableObjectParent = null;

        [SerializeField] private float _displayedObjectFullRotationDuration = 1f;
        
        private GameObject _instantiatedObject;

        private EInventory _activeCategory = EInventory.None;
        
        private TweenerCore<Quaternion, Vector3, QuaternionOptions> _rotationTween;

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

            StartRotationTween();
            
            DisplayItem();

            RegisterToInventoryCategory();

            RegisterToRefreshRequestors();

            base.ActivatedCustomActions();
        }

        protected override void DeactivatedCustomActions()
        {
            DestroyInstantiatedObject();

            StopRotationTween();
            
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

            DisplayItem();
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
            DisplayItem();
        }

        private void StartRotationTween()
        {
            StopRotationTween();

            _rotationTween = _displayableObjectParent.DOLocalRotate(new Vector3(0, 360, 0),
                    _displayedObjectFullRotationDuration, RotateMode.LocalAxisAdd)
                .SetLoops(-1, LoopType.Incremental).SetRelative().SetEase(Ease.Linear);
        }

        private void StopRotationTween()
        {
            _rotationTween?.Kill();
        }

        private void DisplayItem()
        {
            DestroyInstantiatedObject();
            
            bool hasMapping = _InventoryDataProviderMapping.ContainsKey(_activeCategory);

            if (!hasMapping)
            {
                return;
            }
            
            InventoryItemUIData data = _InventoryDataProviderMapping[_activeCategory].GetInventoryItemUIData();

            if (data.UIModel == null)
            {
                return;
            }

            _instantiatedObject = Instantiate(data.UIModel, _displayableObjectParent);
        }

        private void DestroyInstantiatedObject()
        {
            DestroyImmediate(_instantiatedObject);
        }
    }
}