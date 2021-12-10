using System;
using System.Linq;
using UnityEngine;

namespace BLUE.ProjectX.Ultimate
{
    [Serializable]
    public class Settings_Ultimate
    {
        [SerializeField] private EUltimate _ultimateType = default;
        public EUltimate UltimateType => _ultimateType;

        [SerializeField] private UltimateBase _ultimatePrefab = null;
        public UltimateBase UltimatePrefab => _ultimatePrefab;
    }

    [CreateAssetMenu(fileName = "SettingsSO_Ultimate", menuName = "BLUE/Settings/Ultimate/Create SettingsSO_Ultimate")]
    public class SettingsSO_Ultimate : ScriptableObject
    {
        [SerializeField] private Settings_Ultimate[] _ultimateSettingsColl = null;
        
        public bool TryGetUltimateSettings(EUltimate ultimateType, out Settings_Ultimate settings)
        {
            settings = _ultimateSettingsColl.FirstOrDefault(val 
                => val.UltimateType.Equals(ultimateType));

            return settings != null;
        }
    }
}