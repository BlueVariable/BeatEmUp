using System;
using BLUE.InventorySystem;
using UnityEngine;

namespace BLUE.ProjectX.Inventory
{
    public enum EMeleeWeaponItem
    {
        None = 0,
        
        Knife = 1,
        Sword = 2
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
    }
}