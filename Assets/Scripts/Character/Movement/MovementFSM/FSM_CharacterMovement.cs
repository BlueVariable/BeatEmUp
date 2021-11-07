using System.Collections.Generic;
using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character
{
    public enum ECharacterMovementState
    {
        None = 0,
        Idle = 1,
        Move = 2,
        Dash = 3
    }
    
    public class FSMTransitionMessage_CharacterMovement : FSMTransitionMessageBase { }
    
    public class FSM_CharacterMovement : FSMBase<ECharacterMovementState, 
        FSMTransitionMessage_CharacterMovement, FSMTransition_CharacterMovement>
    {
        protected override List<FSMTransition_CharacterMovement> GetInitialPossibleTransitions()
        {
            return new List<FSMTransition_CharacterMovement>()
            {
                new FSMTransition_CharacterMovement(ECharacterMovementState.Idle, ECharacterMovementState.Move),
                new FSMTransition_CharacterMovement(ECharacterMovementState.Idle, ECharacterMovementState.Dash),
                
                new FSMTransition_CharacterMovement(ECharacterMovementState.Move, ECharacterMovementState.Idle),
                new FSMTransition_CharacterMovement(ECharacterMovementState.Move, ECharacterMovementState.Dash),
                
                new FSMTransition_CharacterMovement(ECharacterMovementState.Dash, ECharacterMovementState.Idle),
            };
        }
    }
}
