using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterPlatform_Idle : StateBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform>
    {
        [SerializeField] private FSM_CharacterMovement _movementFSM = null;

        [SerializeField] private AnimationController_Character _animationController = null;

        [SerializeField] private float _fixedTransitionDuration = 0.25f;
        
        public override ECharacterPlatformState GetStateType()
        {
            return ECharacterPlatformState.Idle;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterPlatform transitionMessage)
        {
            _movementFSM.TryGetCurStateType(out ECharacterMovementState movementState);

            if (movementState.Equals(ECharacterMovementState.Move))
            {
                _animationController.TryCrossFadeAnimation(ECharacterAnimation.MOVE, 
                    _fixedTransitionDuration, 0, false);
            }
            else
            {
                _animationController.TryCrossFadeAnimation(ECharacterAnimation.IDLE, 
                    _fixedTransitionDuration, 0, false);
            }
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}