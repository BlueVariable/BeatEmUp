using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Movement
{
    public class FSMTransition_CharacterMovement : FSMTransitionBase<EState>
    {
        public FSMTransition_CharacterMovement(EState initialStateType, EState targetStateType) :
            base(initialStateType, targetStateType)
        {
        }
    }
}