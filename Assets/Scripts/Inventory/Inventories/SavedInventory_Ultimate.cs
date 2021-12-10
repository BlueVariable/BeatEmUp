using System;
using System.Collections.Generic;
using System.Linq;
using BLUE.InventorySystem;
using BLUE.ProjectX.Ultimate;
using UnityEngine;

namespace BLUE.ProjectX.Inventory
{
    [Serializable]
    public class ItemData_Ultimate : ItemData
    {
        [SerializeField] private bool _isEquipped = false;
        public bool IsEquipped => _isEquipped;

        public ItemData_Ultimate(bool isEquipped)
        {
            _isEquipped = isEquipped;
        }
    }
    
    public class SavedInventory_Ultimate : SavedInventoryBase<EUltimate, ItemData_Ultimate, EInventory>
    {
        public override EInventory GetInventoryType()
        {
            return EInventory.Ultimate;
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
                new Item(EUltimate.Heal, new ItemData_Ultimate(true))
            };
        }
        
        public bool TryGetEquippedUltimate(out EUltimate ultimateType)
        {
            ultimateType = default;
            
            Item item = _ItemCollWrapper.Items.SingleOrDefault(val => val.ItemData.IsEquipped);

            if (item != null)
            {
                ultimateType = item.ItemType;
            }

            return item != null;
        }
    }
}