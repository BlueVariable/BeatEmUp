using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_InAir : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterPlatform _platformFSM = null;

        [SerializeField] private AnimationController_Character _characterAnimationController = null;

        [SerializeField] private float _transitionFixedDuration = 0.25f;
        
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

        protected override void AppearStatusCustomActions()
        {
            _characterAnimationController.TryCrossFadeAnimation(ECharacterAnimation.JUMP_LOOP, _transitionFixedDuration);
            
            base.AppearStatusCustomActions();
        }
    }
}