using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_InDashing : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterMovement _characterMovementFSM = null;
     
        [SerializeField] private AnimationController_Character _characterAnimationController = null;

        [SerializeField] private float _transitionFixedDuration = 0.25f;

        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.Dashing;
        }

        public override bool CheckStatus()
        {
            _characterMovementFSM.TryGetCurStateType(out ECharacterMovementState curMovementState);

            return curMovementState == ECharacterMovementState.Dash;
        }

        protected override void AppearStatusCustomActions()
        {
            _characterAnimationController.TryCrossFadeAnimation(ECharacterAnimation.DASH, _transitionFixedDuration);
        }
    }
}