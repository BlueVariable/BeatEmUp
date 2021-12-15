using BLUE.ProjectX.Inventory;
using BLUE.ProjectX.Weapon;

namespace BLUE.ProjectX.UI
{
    public class InventoryItemUIDataProvider_RangedWeapon : InventoryItemUIDataProviderBase<ERangedWeaponItem>
    {
        public override InventoryItemUIData GetItemUIData(ERangedWeaponItem itemType)
        {
            SettingsContainer_RangedWeapon.Instance.RangedWeaponSettingsColl.TryGetRangedWeaponSettings(
                itemType, out Settings_RangedWeapon rangedWeaponSettings);
                
            UISettingsContainer_Weapon.Instance.WeaponUISettingsSO
                .TryGetWeaponUISettings(rangedWeaponSettings.WeaponType, out UISettings_Weapon settings);

            return new InventoryItemUIData(settings.Icon);
        }
    }
}