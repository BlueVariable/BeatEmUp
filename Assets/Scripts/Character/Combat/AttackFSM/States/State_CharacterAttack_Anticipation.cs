using BLUE.FSMSystem;
using BLUE.InputSystem;
using Extensions;
using UnityEngine;

namespace BLUE.ProjectX.Character.States
{
    public class State_CharacterAttack_Anticipation : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        #region Inputs

        private const int _MELEE_ATTACK_BUTTON = 0;
        private const int _RANGED_ATTACK_BUTTON = 1;

        #endregion

        [SerializeField] private FSM_CharacterAttack _attackFSM = null;
        
        [SerializeField] private CharacterAttackSettings _meleeSettings = default;

        [SerializeField] private CharacterAttackSettings _rangedSettings = default;

        [SerializeField] private AnimationController_Character _characterAnimationController = null;

        private FSMTransitionMessage_CharacterAttack _lastTransitionMessage;

        public FSMTransitionMessage_CharacterAttack LastTransitionMessage => _lastTransitionMessage;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.Anticipation;
        }
        
        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            _lastTransitionMessage = transitionMessage;
            
            ECharacterAnimation targetAnimType = transitionMessage.AttackType == EAttackType.Melee
                ? ECharacterAnimation.MELEE_ATTACK
                : ECharacterAnimation.RANGED_ATTACK;

            string fullPath =
                _characterAnimationController.GetFullPathOfState(targetAnimType,
                    AnimationController_Character.ATTACK_LAYER);

            AnimationStateEventBehaviour eventBehaviour = _characterAnimationController.Animator.TryGetBehaviour<AnimationStateEventBehaviour>(fullPath);
            
            eventBehaviour.AddCallback(
                new CharacterAnimationCallback(
                    OnAnticipationCompleted,
                    transitionMessage.AttackSettings.AnticipationTime));
        }

        private void OnAnticipationCompleted()
        {
            if (_lastTransitionMessage.AttackType == EAttackType.Melee)
            {
                _attackFSM.TryChangeState(ECharacterAttackState.MeleeAttack, _lastTransitionMessage);
            }
            else if (_lastTransitionMessage.AttackType == EAttackType.Ranged)
            {
                _attackFSM.TryChangeState(ECharacterAttackState.RangedAttack, _lastTransitionMessage);
            }
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
            InputManager.Instance.TryRegisterToInputListener<InputData_Mouse>(EInputEvent.MouseButtonDown, OnMouseButtonDown);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance)
            {
                InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(EInputEvent.MouseButtonDown,
                    OnMouseButtonDown);
            }
        }

        private void OnMouseButtonDown(InputData_Mouse inputData)
        {
            if (inputData.MouseButton == _MELEE_ATTACK_BUTTON)
            {
                _attackFSM.TryChangeState(
                    ECharacterAttackState.Anticipation,
                    new FSMTransitionMessage_CharacterAttack(
                        EAttackType.Melee,
                        _meleeSettings));
            }
            else if (inputData.MouseButton == _RANGED_ATTACK_BUTTON)
            {
                _attackFSM.TryChangeState(
                    ECharacterAttackState.Anticipation,
                    new FSMTransitionMessage_CharacterAttack(
                        EAttackType.Ranged,
                        _rangedSettings));
            }
        }

        #endregion
    }
}