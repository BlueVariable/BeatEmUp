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
        Climb = 4
    }
    
    public class PlatformFSMTransitionMessage : FSMTransitionMessageBase { }
    
    public class PlatformFSM : FSMBase<EState, PlatformFSMTransitionMessage, PlatformFSMTransition>
    {
        // TODO : Platform FSM needs Fall state 
        // When release climb without jumping 
        // Need Transition Climb -> Jump and Requires Cooldown to not again climb instantly
        
        protected override List<PlatformFSMTransition> GetInitialPossibleTransitions()
        {
            return new List<PlatformFSMTransition>()
            {
                new PlatformFSMTransition(EState.Idle, EState.Jump),

                new PlatformFSMTransition(EState.Jump, EState.Idle),
                new PlatformFSMTransition(EState.Jump, EState.DoubleJump),
                new PlatformFSMTransition(EState.Jump, EState.Climb),
                
                new PlatformFSMTransition(EState.DoubleJump, EState.Idle),
                new PlatformFSMTransition(EState.DoubleJump, EState.Climb),
                
                new PlatformFSMTransition(EState.Climb, EState.Jump),
                new PlatformFSMTransition(EState.Climb, EState.Idle),
            };
        }
    }
}