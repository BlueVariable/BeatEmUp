using System;
using BLUE.DialogueSystem;

namespace BLUE.ProjectX.Dialogue
{
    public class DialoguePlayer_SimpleSpeaker : DialoguePlayer_SimpleSpeakerBase<ESimpleSpeakerDialogue, ESimpleSpeaker>
    {
        private static DialoguePlayer_SimpleSpeaker _instance;
        public static DialoguePlayer_SimpleSpeaker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<DialoguePlayer_SimpleSpeaker>();
                }

                return _instance;
            }
        }
    }
    
    [Serializable]
    public class DialogueData_SimpleSpeaker : DialogueData_SimpleSpeaker<ESimpleSpeaker>
    {
    }
    
    public enum ESimpleSpeaker
    {
        None = 0,
        
        Narrative = 1,

        Tutorial = 2,
        
        Yu = 100,
        
        Overseer = 200,
        
        Z = 300,
        
        Yu_Dialogue = 400,
        Z_Dialogue = 500
    }
    
    public enum ESimpleSpeakerDialogue
    {
        None = 0,
        
        Tutorial_BasicMove_01 = 10001,
        Tutorial_Jump_01 = 10002,
        Tutorial_Dash_01 = 10003,
        
        Tutorial_Combat_Melee_01 = 10011,
        Tutorial_Combat_Range_01 = 10021,

        Narrative_Level01_01 = 20001,
        Narrative_Level01_02 = 20002,
        
        Narrative_Level_02_01 = 20101,
        Narrative_Level_02_02 = 20102,
        
        Dialogue_Level01_01 = 30001,
        Dialogue_Level01_02 = 30002,
        Dialogue_Level01_03 = 30003,
    }
}