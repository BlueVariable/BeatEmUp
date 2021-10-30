using System.Collections.Generic;
using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Platform
{
    public enum ECharacterPlatformState
    {
        None = 0,
        Idle = 1,
        Jump = 2,
        DoubleJump = 3,
        Climb = 4
    }
    
    public class FSMTransitionMessage_CharacterPlatform : FSMTransitionMessageBase { }
    
    public class FSM_CharacterPlatform : FSMBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform, FSMTransition_CharacterPlatform>
    {
        // TODO : Platform FSM needs Fall state 
        protected override List<FSMTransition_CharacterPlatform> GetInitialPossibleTransitions()
        {
            return new List<FSMTransition_CharacterPlatform>()
            {
                new FSMTransition_CharacterPlatform(ECharacterPlatformState.Idle, ECharacterPlatformState.Jump),

                new FSMTransition_CharacterPlatform(ECharacterPlatformState.Jump, ECharacterPlatformState.Idle),
                new FSMTransition_CharacterPlatform(ECharacterPlatformState.Jump, ECharacterPlatformState.DoubleJump),
                
                new FSMTransition_CharacterPlatform(ECharacterPlatformState.DoubleJump, ECharacterPlatformState.Idle),
            };
        }
    }
}