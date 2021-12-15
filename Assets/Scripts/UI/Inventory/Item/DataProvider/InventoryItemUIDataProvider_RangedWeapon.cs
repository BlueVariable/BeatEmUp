using BLUE.Common;
using BLUE.ProjectX.Inventory;
using BLUE.ProjectX.Weapon;

namespace BLUE.ProjectX.UI
{
    public class InventoryItemUIDataProvider_RangedWeapon : InventoryItemUIDataProviderBase<ERangedWeaponItem>
    {
        #region Constants

        private const string AP = "AP";
        
        private const string PROJECTILE_COUNT = "BULLET";

        #endregion
        
        public override InventoryItemUIData GetItemUIData(ERangedWeaponItem itemType)
        {
            SettingsContainer_RangedWeapon.Instance.RangedWeaponSettingsColl.TryGetRangedWeaponSettings(
                itemType, out Settings_RangedWeapon rangedWeaponSettings);
                
            UISettingsContainer_Weapon.Instance.WeaponUISettingsSO
                .TryGetWeaponUISettings(rangedWeaponSettings.WeaponType, out UISettings_Weapon settings);
            
            SettingsContainer_WeaponAttack.Instance.GetAttackSettings(rangedWeaponSettings.WeaponType,
                out Settings_WeaponAttack<AttackData_ProjectileThrower> attackSettings);

            string statDescription = AP + ": " + attackSettings.AttackDataColl[0].Damage + "<br>"
                                     + PROJECTILE_COUNT + ": " + attackSettings.AttackDataColl.Length;

            return new InventoryItemUIData(settings.DisplayName, settings.Description, 
                statDescription, settings.Icon);
        }
    }
}