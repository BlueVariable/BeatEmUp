using BLUE.FSMSystem;
using BLUE.ProjectX.Character.Movement;
using Character.Animation;
using UnityEngine;

namespace BLUE.ProjectX.Character.Platform
{
    public class State_CharacterPlatform_Idle : StateBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform>
    {
        [SerializeField] private FSM_CharacterMovement _movementFSM = null;

        [SerializeField] private CharacterAnimationController _animationController = null;
        
        public override ECharacterPlatformState GetStateType()
        {
            return ECharacterPlatformState.Idle;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterPlatform transitionMessage)
        {
            _movementFSM.TryGetCurStateType(out ECharacterMovementState movementState);

            if (movementState == ECharacterMovementState.Move)
            {
                _animationController.TryCrossFadeAnimation(ECharacterAnimation.MOVE, 0.25f, 0, false);

            }
            else
            {
                _animationController.TryCrossFadeAnimation(ECharacterAnimation.IDLE, 0.25f, 0, false);
            }
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}