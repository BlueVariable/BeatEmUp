using System;
using BLUE.FSMSystem;
using BLUE.InputSystem;
using BLUE.ProjectX.Character;
using UnityEngine;

namespace Character.Combat.AttackFSM.States
{
    public class State_CharacterAttack_MeleeAttack : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private FSM_CharacterAttack _characterAttackFSM = null;

        [SerializeField] private AnimationController_Character _characterAnimationController = null;

        [SerializeField] private float _transitionDuration = 0.25f;
        
        [SerializeField] private int _targetMouseButton = 0;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.MeleeAttack;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            // weapon attack ??

            _characterAnimationController.TryCrossFadeAnimation(ECharacterAnimation.MELEE_ATTACK, _transitionDuration, 1);

            _characterAnimationController.OnAnimationPeriodCompleted += OnAnimPeriodCompleted;
            
            base.EnterStateCustomActions(transitionMessage);
        }

        private void OnAnimPeriodCompleted(ECharacterAnimation eCharacterAnimation, int layerIndex, int currentLoop)
        {
            if (eCharacterAnimation != ECharacterAnimation.MELEE_ATTACK)
            {
                return;
            }
            
            _characterAnimationController.OnAnimationPeriodCompleted -= OnAnimPeriodCompleted;

            _characterAttackFSM.TryChangeState(ECharacterAttackState.Idle);
        }

        #region Input Related

        private void Awake()
        {
            RegisterToInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Mouse>(EInputEvent.MouseButtonDown,
                OnMouseButtonDown);
        }

        private void UnregisterFromInputManager()
        {
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(EInputEvent.MouseButtonDown,
                OnMouseButtonDown);
        }

        private void OnMouseButtonDown(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _targetMouseButton)
            {
                return;
            }

            _characterAttackFSM.TryChangeState(ECharacterAttackState.MeleeAttack);
        }

        #endregion
    }
}