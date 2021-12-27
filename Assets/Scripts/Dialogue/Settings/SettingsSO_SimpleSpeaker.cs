using System;
using System.Linq;
using UnityEngine;

namespace BLUE.ProjectX.Dialogue
{
    [CreateAssetMenu(fileName = "SettingsSO_SimpleSpeaker", 
        menuName = "BLUE/Settings/Dialogue/Create SettingsSO_SimpleSpeaker")]
    public class SettingsSO_SimpleSpeaker : ScriptableObject
    {
        [Serializable]
        public class SpeakerSetting
        {
            [SerializeField] private ESimpleSpeaker _speaker = default;
            public ESimpleSpeaker Speaker => _speaker;
            
            [SerializeField] private string _speakerName = null;
            public string SpeakerName => _speakerName;
        }

        [SerializeField] private SpeakerSetting[] _speakerSettings = new SpeakerSetting[0];
        
        public bool TryGetSpeakerSettings(
            ESimpleSpeaker speaker,
            out SpeakerSetting speakerSetting)
        {
            speakerSetting = _speakerSettings
                .FirstOrDefault(i => i.Speaker.Equals(speaker));

            return speakerSetting != null;
        }
    }
}