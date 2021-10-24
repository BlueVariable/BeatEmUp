using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Platform
{
    public class PlatformFSMTransition : FSMTransitionBase<EState>
    {
        public PlatformFSMTransition(EState initialStateType, EState targetStateType) :
            base(initialStateType, targetStateType)
        {
        }
    }
}