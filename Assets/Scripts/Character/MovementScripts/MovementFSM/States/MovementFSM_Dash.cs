using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Movement
{
    public class MovementFSM_Dash : StateBase<EState, MovementFSMTransitionMessage>
    {
        public override EState GetStateType()
        {
            return EState.Dash;
        }
    }
}