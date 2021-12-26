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
    }
    
    public enum ESimpleSpeakerDialogue
    {
        None = 0,
        
        Tutorial_BasicMove_01 = 10001,

        Narrative_Level01_01 = 20001,
    }
}