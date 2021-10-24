using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Platform
{
    public class PlatformFSM_IdleState : StateBase<EState, PlatformFSMTransitionMessage, PlatformFSMTransition>
    {
        public override EState GetStateType()
        {
            return EState.Idle;
        }
    }
}