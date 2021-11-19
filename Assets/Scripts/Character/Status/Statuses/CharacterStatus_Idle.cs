using BLUE.Helpers;
using BLUE.ProjectX.Character;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_Idle : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterMovement _movementFSM = null;

        [SerializeField] private FSM_CharacterPlatform _platformFSM = null;
        
        [SerializeField] private AnimationController_Character _characterAnimationController = null;

        [SerializeField] private float _transitionFixedDuration = 0.25f;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.Idle;
        }

        public override bool CheckStatus()
        {
            _movementFSM.TryGetCurStateType(out ECharacterMovementState curMovementState);
            _platformFSM.TryGetCurStateType(out ECharacterPlatformState curPlatformState);

            return curMovementState == ECharacterMovementState.Idle &&
                   curPlatformState == ECharacterPlatformState.Idle;
        }

        protected override void AppearStatusCustomActions()
        {
            CoroutineHelper.Instance.WaitForSeconds(0.05f, () =>
            {
                if (IsAppearing)
                {
                    _characterAnimationController.TryCrossFadeAnimation(
                        ECharacterAnimation.IDLE,
                        _transitionFixedDuration,
                        AnimationController_Character.MOVEMENT_LAYER,
                        false);
                }
            });
            
            base.AppearStatusCustomActions();
        }
    }
}