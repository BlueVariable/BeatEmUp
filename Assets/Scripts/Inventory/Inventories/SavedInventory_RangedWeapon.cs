using System;
using System.Collections.Generic;
using System.Linq;
using BLUE.InventorySystem;
using UnityEngine;

namespace BLUE.ProjectX.Inventory
{
    public enum ERangedWeaponItem
    {
        None = 0,
        
        FireSpell = 1,
        IceSpell = 2
    }

    [Serializable]
    public class ItemData_RangedWeapon : ItemData
    {
        [SerializeField] private bool _isEquipped = false;
        public bool IsEquipped => _isEquipped;

        public ItemData_RangedWeapon(bool isEquipped)
        {
            _isEquipped = isEquipped;
        }
    }
    
    public class SavedInventory_RangedWeapon : SavedInventoryBase<ERangedWeaponItem, ItemData_RangedWeapon, EInventory>
    {
        public override EInventory GetInventoryType()
        {
            return EInventory.RangedWeapon;
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
                new Item(ERangedWeaponItem.FireSpell, new ItemData_RangedWeapon(true)),
                new Item(ERangedWeaponItem.IceSpell, new ItemData_RangedWeapon(false))
            };
        }
        
        public bool TryGetEquippedWeapon(out ERangedWeaponItem itemType)
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
