using System;
using BLUE.InventorySystem;
using UnityEngine;

namespace BLUE.ProjectX.Inventory
{
    public enum ERangedWeaponItem
    {
        None = 0,
    }

    [Serializable]
    public class ItemData_RangedWeapon : ItemData
    {
        [SerializeField] private bool _owned = false;
        public bool Owned => _owned;

        public ItemData_RangedWeapon(bool owned)
        {
            _owned = owned;
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
                Debug.Log("<color=yellow>Inventory Debug:</color> "+ item.ItemType + ": " + item.ItemData.Owned);
            }
        }
    }
}
