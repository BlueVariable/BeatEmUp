using System.Collections.Generic;
using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character
{
    public enum ECharacterPlatformState
    {
        None = 0,
        Idle = 1,
        Jump = 2,
        DoubleJump = 3,
        Death = 4
    }
    
    public class FSMTransitionMessage_CharacterPlatform : FSMTransitionMessageBase { }
    
    public class FSM_CharacterPlatform : FSMBase<ECharacterPlatformState, 
        FSMTransitionMessage_CharacterPlatform, FSMTransition_CharacterPlatform>
    {
        protected override List<FSMTransition_CharacterPlatform> GetInitialPossibleTransitions()
        {
            return new List<FSMTransition_CharacterPlatform>()
            {
                new FSMTransition_CharacterPlatform(ECharacterPlatformState.Idle, ECharacterPlatformState.Jump),
                new FSMTransition_CharacterPlatform(ECharacterPlatformState.Idle, ECharacterPlatformState.Death),

                new FSMTransition_CharacterPlatform(ECharacterPlatformState.Jump, ECharacterPlatformState.Idle),
                new FSMTransition_CharacterPlatform(ECharacterPlatformState.Jump, ECharacterPlatformState.DoubleJump),
                new FSMTransition_CharacterPlatform(ECharacterPlatformState.Jump, ECharacterPlatformState.Death),
                
                new FSMTransition_CharacterPlatform(ECharacterPlatformState.DoubleJump, ECharacterPlatformState.Idle),
                new FSMTransition_CharacterPlatform(ECharacterPlatformState.DoubleJump, ECharacterPlatformState.Death),
            };
        }
    }
}