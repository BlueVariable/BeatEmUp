using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_InAir : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterPlatform _platformFSM = null;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.InAir;
        }

        public override bool CheckStatus()
        {
            _platformFSM.TryGetCurStateType(out ECharacterPlatformState curPlatformState);

            return curPlatformState == ECharacterPlatformState.Jump ||
                   curPlatformState == ECharacterPlatformState.DoubleJump;
        }

        public override void AppearStatus()
        {
            // play jump anim
        }

        public override void DisappearStatus()
        {
            // do nothing for now
        }
    }
}