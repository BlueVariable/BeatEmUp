using BLUE.Common;
using BLUE.ProjectX.Inventory;
using BLUE.ProjectX.Weapon;

namespace BLUE.ProjectX.UI
{
    public class InventoryItemUIDataProvider_MeleeWeapon : InventoryItemUIDataProviderBase<EMeleeWeaponItem>
    {
        #region Constants

        private const string AP = "AP";
        
        private const string RANGE = "RANGE";

        #endregion
        
        public override InventoryItemUIData GetItemUIData(EMeleeWeaponItem itemType)
        {
            SettingsContainer_MeleeWeapon.Instance.MeleeWeaponSettingsColl.TryGetMeleeWeaponSettings(
                itemType, out Settings_MeleeWeapon meleeWeaponSettings);
                
            UISettingsContainer_Weapon.Instance.WeaponUISettingsSO
                .TryGetWeaponUISettings(meleeWeaponSettings.WeaponType, out UISettings_Weapon settings);
            
            SettingsContainer_WeaponAttack.Instance.GetAttackSettings(meleeWeaponSettings.WeaponType,
                out Settings_WeaponAttack<AttackData_AoE> attackSettings);

            string statDescription = AP + ": " + attackSettings.AttackDataColl[0].Damage + "<br>"
                                     + RANGE + ": " + attackSettings.AttackDataColl[0].Radius;

            return new InventoryItemUIData(settings.DisplayName, settings.Description, 
                statDescription, settings.Icon, settings.UIModel);
        }
    }
}