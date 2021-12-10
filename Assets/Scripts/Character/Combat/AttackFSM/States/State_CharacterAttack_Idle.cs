using System;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterAttack_Idle : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private FSM_CharacterMovement _movementFSM = null;

        [SerializeField] private FSM_CharacterAttack _attackFSM = null;
        
        [SerializeField] private AnimationController_Character _characterAnimationController = null;

        [SerializeField] private float _fixedTransitionDuration = 0.1f;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.Idle;
        }
        
        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            _characterAnimationController.TryCrossFadeAnimation(
                ECharacterAnimation.ATTACK_EMPTY,
                _fixedTransitionDuration,
                AnimationController_Character.ATTACK_LAYER,
                false);
            
            base.EnterStateCustomActions(transitionMessage);
        }

        private void Awake()
        {
            RegisterToMovementFSM();
        }

        private void OnDestroy()
        {
            UnregisterFromMovementFSM();
        }

        #region Attack Cancel Related

        private void RegisterToMovementFSM()
        {
            _movementFSM.OnStateEntered += OnStateEntered;
        }

        private void UnregisterFromMovementFSM()
        {
            _movementFSM.OnStateEntered -= OnStateEntered;
        }

        private void OnStateEntered(ECharacterMovementState movementState)
        {
            if (movementState == ECharacterMovementState.Dash)
            {
                Debug.Log("Change State to Idle");
                
                _attackFSM.TryChangeState(ECharacterAttackState.Idle);
            }
        }

        #endregion
    }
}