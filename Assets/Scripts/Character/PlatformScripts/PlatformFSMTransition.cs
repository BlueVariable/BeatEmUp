using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Platform
{
    public class PlatformFSMTransition : FSMTransition<EState>
    {
        public PlatformFSMTransition(EState initialStateType, EState targetStateType) :
            base(initialStateType, targetStateType)
        {
        }
    }
}