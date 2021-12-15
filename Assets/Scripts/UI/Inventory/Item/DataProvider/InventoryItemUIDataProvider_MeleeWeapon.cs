using BLUE.ProjectX.Inventory;
using BLUE.ProjectX.Weapon;

namespace BLUE.ProjectX.UI
{
    public class InventoryItemUIDataProvider_MeleeWeapon : InventoryItemUIDataProviderBase<EMeleeWeaponItem>
    {
        public override InventoryItemUIData GetItemUIData(EMeleeWeaponItem itemType)
        {
            SettingsContainer_MeleeWeapon.Instance.MeleeWeaponSettingsColl.TryGetMeleeWeaponSettings(
                itemType, out Settings_MeleeWeapon meleeWeaponSettings);
                
            UISettingsContainer_Weapon.Instance.WeaponUISettingsSO
                .TryGetWeaponUISettings(meleeWeaponSettings.WeaponType, out UISettings_Weapon settings);

            return new InventoryItemUIData(settings.DisplayName, settings.Description, settings.Icon);
        }
    }
}