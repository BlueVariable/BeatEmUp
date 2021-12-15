using System;
using System.Linq;
using BLUE.ProjectX.Ultimate;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    [Serializable]
    public class UISettings_Ultimate
    {
        [SerializeField] private EUltimate _ultimateType = default;
        public EUltimate UltimateType => _ultimateType;
        
        [SerializeField] private string _displayName = null;
        public string DisplayName => _displayName;

        [SerializeField] private string _description = null;
        public string Description => _description;
        
        [SerializeField] private Sprite _icon = null;
        public Sprite Icon => _icon;
    }

    [CreateAssetMenu(fileName = "UISettingsSO_Ultimate", menuName = "BLUE/Settings/UI/Ultimate/Create UISettingsSO_Ultimate")]
    public class UISettingsSO_Ultimate : ScriptableObject
    {
        [SerializeField] private UISettings_Ultimate[] _ultimateUISettingsColl = null;
        
        public bool TryGetUltimateUISettings(EUltimate ultimateType, out UISettings_Ultimate settings)
        {
            settings = _ultimateUISettingsColl.FirstOrDefault(val 
                => val.UltimateType.Equals(ultimateType));

            return settings != null;
        }
    }
}
