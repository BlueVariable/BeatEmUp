using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Platform.States
{
    public class PlatformFSM_IdleState : StateBase<EState, PlatformFSMTransitionMessage>
    {
        public override EState GetStateType()
        {
            return EState.Idle;
        }
    }
}