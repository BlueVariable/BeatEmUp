using System;
using BLUE.ProjectX.Ultimate;

namespace BLUE.ProjectX.UI
{
    public class InventoryItemUIDataProvider_Ultimate : InventoryItemUIDataProviderBase<EUltimate>
    {
        public override InventoryItemUIData GetItemUIData(EUltimate itemType)
        {
            UISettingsContainer_Ultimate.Instance.UltimateUISettingsSO
                .TryGetUltimateUISettings(itemType, out UISettings_Ultimate settings);

            return new InventoryItemUIData(settings.DisplayName, settings.Description, 
                String.Empty, settings.Icon);
        }
    }
}