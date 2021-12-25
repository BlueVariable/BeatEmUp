using BLUE.DialogueSystem;
using UnityEngine;

namespace BLUE.ProjectX.Dialogue
{
    [CreateAssetMenu(fileName = "SettingsSO_DialogueDataProvider_SimpleSpeaker", 
        menuName = "BLUE/Settings/Dialogue/Create SettingsSO_DialogueDataProvider_SimpleSpeaker")]
    public class SettingsSO_DialogueDataProvider_SimpleSpeaker : 
        SettingsSO_DialogueDataProviderBase<ESimpleSpeakerDialogue, DialogueData_SimpleSpeaker<ESimpleSpeaker>>
    {
    }
}