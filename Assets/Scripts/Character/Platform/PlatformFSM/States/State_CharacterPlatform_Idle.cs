using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterPlatform_Idle : StateBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform>
    {
        public override ECharacterPlatformState GetStateType()
        {
            return ECharacterPlatformState.Idle;
        }
    }
}