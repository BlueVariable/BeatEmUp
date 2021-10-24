using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Movement
{
    public class MovementFSMTransition : FSMTransitionBase<EState>
    {
        public MovementFSMTransition(EState initialStateType, EState targetStateType) :
            base(initialStateType, targetStateType)
        {
        }
    }
}