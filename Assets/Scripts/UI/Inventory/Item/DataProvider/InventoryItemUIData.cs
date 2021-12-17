using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class InventoryItemUIData
    {
        public string DisplayName { get; }
        public string Description { get; }
        public string StatDescription { get; }
        public Sprite ItemIcon { get; }
        public GameObject UIModel { get; }

        public InventoryItemUIData(string displayName, string description, string statDescription, 
            Sprite itemIcon, GameObject uiModel = null)
        {
            DisplayName = displayName;
            Description = description;
            StatDescription = statDescription;
            ItemIcon = itemIcon;
            UIModel = uiModel;
        }
    }
}