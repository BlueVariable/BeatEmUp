using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class InventoryItemUIData
    {
        public Sprite ItemIcon { get; private set; }

        public InventoryItemUIData(Sprite itemIcon)
        {
            ItemIcon = itemIcon;
        }
    }
}