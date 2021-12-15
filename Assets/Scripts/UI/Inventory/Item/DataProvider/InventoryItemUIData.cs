using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class InventoryItemUIData
    {
        public string DisplayName { get; }
        public string Description { get; }
        public Sprite ItemIcon { get; }

        public InventoryItemUIData(string displayName, string description, Sprite itemIcon)
        {
            DisplayName = displayName;
            Description = description;
            ItemIcon = itemIcon;
        }
    }
}