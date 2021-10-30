using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Movement
{
    public class FSMTransition_CharacterMovement : FSMTransitionBase<ECharacterMovementState>
    {
        public FSMTransition_CharacterMovement(ECharacterMovementState initialCharacterMovementStateType, ECharacterMovementState targetCharacterMovementStateType) :
            base(initialCharacterMovementStateType, targetCharacterMovementStateType)
        {
        }
    }
}