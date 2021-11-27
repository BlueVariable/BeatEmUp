using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_Dashing : CharacterStatusBase
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

        protected override void StatusAppearedCustomActions()
        {
            _characterAnimationController.TryCrossFadeAnimation(
                ECharacterAnimation.DASH,
                _transitionFixedDuration,
                AnimationController_Character.MOVEMENT_LAYER,
                false);
        }
    }
}