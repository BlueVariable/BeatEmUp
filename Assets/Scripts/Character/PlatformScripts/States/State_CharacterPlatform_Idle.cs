using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Platform
{
    public class State_CharacterPlatform_Idle : StateBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform, FSMTransition_CharacterPlatform>
    {
        public override ECharacterPlatformState GetStateType()
        {
            return ECharacterPlatformState.Idle;
        }
    }
}