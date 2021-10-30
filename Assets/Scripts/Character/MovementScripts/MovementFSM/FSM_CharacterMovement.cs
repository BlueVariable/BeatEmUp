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
    
    public class FSMTransitionMessage_CharacterMovement : FSMTransitionMessageBase { }
    
    public class FSM_CharacterMovement : FSMBase<EState, FSMTransitionMessage_CharacterMovement, FSMTransition_CharacterMovement>
    {
        protected override List<FSMTransition_CharacterMovement> GetInitialPossibleTransitions()
        {
            return new List<FSMTransition_CharacterMovement>()
            {
                new FSMTransition_CharacterMovement(EState.Idle, EState.Move),
                new FSMTransition_CharacterMovement(EState.Idle, EState.Dash),
                
                new FSMTransition_CharacterMovement(EState.Move, EState.Idle),
                new FSMTransition_CharacterMovement(EState.Move, EState.Dash),
                
                new FSMTransition_CharacterMovement(EState.Dash, EState.Idle),
            };
        }
    }
}
