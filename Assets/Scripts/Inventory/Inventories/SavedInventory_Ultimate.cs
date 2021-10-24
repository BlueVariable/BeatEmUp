using System;
using BLUE.InventorySystem;
using UnityEngine;

namespace BLUE.ProjectX.Inventory
{
    public enum EUltimateItem
    {
        None = 0,
    }

    [Serializable]
    public class ItemData_Ultimate : ItemData
    {
        [SerializeField] private bool _owned = false;
        public bool Owned => _owned;

        public ItemData_Ultimate(bool owned)
        {
            _owned = owned;
        }
    }
    
    public class SavedInventory_Ultimate : SavedInventoryBase<EUltimateItem, ItemData_Ultimate, EInventory>
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
                Debug.Log("<color=yellow>Inventory Debug:</color> "+ item.ItemType + ": " + item.ItemData.Owned);
            }
        }
    }
}