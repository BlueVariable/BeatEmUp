using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.Platform
{
    public class FSMTransition_CharacterPlatform : FSMTransitionBase<ECharacterPlatformState>
    {
        public FSMTransition_CharacterPlatform(ECharacterPlatformState initialCharacterPlatformStateType, ECharacterPlatformState targetCharacterPlatformStateType) :
            base(initialCharacterPlatformStateType, targetCharacterPlatformStateType)
        {
        }
    }
}