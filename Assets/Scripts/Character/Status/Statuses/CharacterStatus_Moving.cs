using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_Moving : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterMovement _movementFSM = null;
 
        [SerializeField] private AnimationController_Character _characterAnimationController = null;

        [SerializeField] private float _transitionFixedDuration = 0.25f;

        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.Moving;
        }

        public override bool CheckStatus()
        {
            _movementFSM.TryGetCurStateType(out ECharacterMovementState curMovementState);

            return curMovementState == ECharacterMovementState.Move;
        }

        protected override void AppearStatusCustomActions()
        {
            _characterAnimationController.TryCrossFadeAnimation(ECharacterAnimation.MOVE, _transitionFixedDuration, 0, false);
        }
    }
}