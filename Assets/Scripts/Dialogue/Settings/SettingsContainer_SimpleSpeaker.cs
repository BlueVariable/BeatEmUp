using System;
using UnityEngine;

namespace BLUE.ProjectX.Dialogue
{
    public class SettingsContainer_SimpleSpeaker : MonoBehaviour
    {
        #region Singleton

        private static SettingsContainer_SimpleSpeaker _instance;
        public static SettingsContainer_SimpleSpeaker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SettingsContainer_SimpleSpeaker>();
                }

                return _instance;
            }
        }

        #endregion
        
        [SerializeField] private SettingsSO_SimpleSpeaker _speakerSettingsSO = null;

        public bool TryGetSpeakerSettings(
            ESimpleSpeaker speaker,
            out SettingsSO_SimpleSpeaker.SpeakerSetting speakerSetting)
        {
            return _speakerSettingsSO.TryGetSpeakerSettings(speaker, out speakerSetting);
        }
    }
}