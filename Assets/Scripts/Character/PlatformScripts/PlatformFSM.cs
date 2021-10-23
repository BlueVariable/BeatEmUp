using System.Collections.Generic;
using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Platform
{
    public enum EState
    {
        None = 0,
        Idle = 1,
        Jump = 2,
        DoubleJump = 3,
    }
    
    public class PlatformFSMTransitionMessage : FSMTransitionMessage { }
    
    public class PlatformFSM : FSMBase<EState, PlatformFSMTransitionMessage, PlatformFSMTransition>
    {
        protected override List<PlatformFSMTransition> GetInitialPossibleTransitions()
        {
            return new List<PlatformFSMTransition>()
            {
                new PlatformFSMTransition(EState.Idle, EState.Jump),

                new PlatformFSMTransition(EState.Jump, EState.Idle),
                new PlatformFSMTransition(EState.Jump, EState.DoubleJump),
                
                new PlatformFSMTransition(EState.DoubleJump, EState.Idle),
            };
        }
    }
}