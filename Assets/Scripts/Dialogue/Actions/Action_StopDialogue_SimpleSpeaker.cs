using BLUE.ActionSystem;

namespace BLUE.ProjectX.Dialogue
{
    public class Action_StopDialogue_SimpleSpeaker : ActionBase
    {
        public override void Execute()
        {
            DialoguePlayer_SimpleSpeaker.Instance.Stop();
        }
    }
}