using System.Collections.Generic;
using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Movement
{
    public enum EState
    {
        None = 0,
        Idle = 1,
        Move = 2,
        Dash = 3
    }
    
    public class MovementFSMTransitionMessage : FSMTransitionMessageBase { }
    
    public class MovementFSM : FSMBase<EState, MovementFSMTransitionMessage, MovementFSMTransition>
    {
        protected override List<MovementFSMTransition> GetInitialPossibleTransitions()
        {
            return new List<MovementFSMTransition>()
            {
                new MovementFSMTransition(EState.Idle, EState.Move),
                new MovementFSMTransition(EState.Idle, EState.Dash),
                
                new MovementFSMTransition(EState.Move, EState.Idle),
                new MovementFSMTransition(EState.Move, EState.Dash),
                
                new MovementFSMTransition(EState.Dash, EState.Idle),
            };
        }
    }
}
