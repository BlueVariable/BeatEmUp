using System;

namespace BLUE.ProjectX.UI
{
    public abstract class InventoryItemUIDataProviderBase
    {
        public abstract InventoryItemUIData GetItemUIData(Enum itemType);
    }
    
    public abstract class InventoryItemUIDataProviderBase<T> : InventoryItemUIDataProviderBase
        where T : Enum
    {
        public abstract InventoryItemUIData GetItemUIData(T itemType);
        
        public sealed override InventoryItemUIData GetItemUIData(Enum itemType)
        {
            return GetItemUIData((T) itemType);
        }
    }
}
