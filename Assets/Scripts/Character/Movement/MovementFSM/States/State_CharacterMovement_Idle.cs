using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterMovement_Idle : StateBase<ECharacterMovementState, FSMTransitionMessage_CharacterMovement>
    {
        [SerializeField] private FSM_CharacterPlatform _platformFSM = null;
        
        [SerializeField] private AnimationController_Character _animationController = null;

        [SerializeField] private float _fixedTransitionDuration = 0.25f;
        
        public override ECharacterMovementState GetStateType()
        {
            return ECharacterMovementState.Idle;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterMovement transitionMessage)
        {
            _platformFSM.TryGetCurStateType(out ECharacterPlatformState curPlatformState);

            if (curPlatformState.Equals(ECharacterPlatformState.Idle))
            {
                _animationController.TryCrossFadeAnimation(ECharacterAnimation.IDLE, 
                    _fixedTransitionDuration, 0, false);
            }
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}
