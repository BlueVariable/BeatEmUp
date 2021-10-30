using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Movement
{
    public class State_CharacterMovement_Idle : StateBase<EState, FSMTransitionMessage_CharacterMovement, FSMTransition_CharacterMovement>
    {
        public override EState GetStateType()
        {
            return EState.Idle;
        }
    }
}
