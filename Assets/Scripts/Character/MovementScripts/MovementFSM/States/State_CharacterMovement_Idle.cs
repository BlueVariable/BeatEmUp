using BLUE.FSMSystem;
using Character.Animation;
using UnityEngine;

namespace BLUE.ProjectX.Character.Movement
{
    public class State_CharacterMovement_Idle : StateBase<ECharacterMovementState, FSMTransitionMessage_CharacterMovement>
    {
        [SerializeField] private CharacterAnimationController _animationController = null;
        
        public override ECharacterMovementState GetStateType()
        {
            return ECharacterMovementState.Idle;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterMovement transitionMessage)
        {
            _animationController.TryCrossFadeAnimation(ECharacterAnimation.IDLE, 0.25f, 0, false);
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}
