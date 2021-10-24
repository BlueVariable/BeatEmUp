using System;
using BLUE.InventorySystem;
using UnityEngine;

namespace BLUE.ProjectX.Inventory
{
    public enum ECurrencyItem
    {
        None = 0,
    }

    [Serializable]
    public class ItemData_Currency : ItemData
    {
        [SerializeField] private int _amount = 0;
        public int Amount => _amount;

        public ItemData_Currency(int amount)
        {
            _amount = amount;
        }
    }
    
    public class SavedInventory_Currency : SavedInventoryBase<ECurrencyItem, ItemData_Currency, EInventory>
    {
        public override EInventory GetInventoryType()
        {
            return EInventory.Currency;
        }

        public override void DebugCustomActions()
        {
            base.DebugCustomActions();
            
            foreach (Item item in _ItemCollWrapper.Items)
            {
                Debug.Log("<color=yellow>Inventory Debug:</color> "+ item.ItemType + ": " + item.ItemData.Amount);
            }
        }
    }
}