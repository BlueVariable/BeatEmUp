using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterMovement_Idle : StateBase<ECharacterMovementState, FSMTransitionMessage_CharacterMovement>
    {
        public override ECharacterMovementState GetStateType()
        {
            return ECharacterMovementState.Idle;
        }
    }
}
