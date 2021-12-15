using System;
using System.Collections.Generic;
using System.Linq;
using BLUE.InventorySystem;
using UnityEngine;

namespace BLUE.ProjectX.Inventory
{
    public enum EMeleeWeaponItem
    {
        None = 0,
        
        Sword = 1,
        
        Sword_TEST = 2
    }

    [Serializable]
    public class ItemData_MeleeWeapon : ItemData
    {
        [SerializeField] private bool _isEquipped = false;
        public bool IsEquipped => _isEquipped;

        public ItemData_MeleeWeapon(bool isEquipped)
        {
            _isEquipped = isEquipped;
        }
    }
    
    public class SavedInventory_MeleeWeapon : SavedInventoryBase<EMeleeWeaponItem, ItemData_MeleeWeapon, EInventory>
    {
        public override EInventory GetInventoryType()
        {
            return EInventory.MeleeWeapon;
        }

        public override void DebugCustomActions()
        {
            base.DebugCustomActions();
            
            foreach (Item item in _ItemCollWrapper.Items)
            {
                Debug.Log("<color=yellow>Inventory Debug:</color> "+ item.ItemType + ": " + item.ItemData.IsEquipped);
            }
        }
        
        protected override List<Item> GetFallbackInitialItems()
        {
            return new List<Item>()
            {
                new Item(EMeleeWeaponItem.Sword, new ItemData_MeleeWeapon(true)),
                new Item(EMeleeWeaponItem.Sword_TEST, new ItemData_MeleeWeapon(false))
            };
        }

        public bool TryGetEquippedWeapon(out EMeleeWeaponItem itemType)
        {
            itemType = default;
            
            Item item = _ItemCollWrapper.Items.SingleOrDefault(val => val.ItemData.IsEquipped);

            if (item != null)
            {
                itemType = item.ItemType;
            }

            return item != null;
        }
    }
}