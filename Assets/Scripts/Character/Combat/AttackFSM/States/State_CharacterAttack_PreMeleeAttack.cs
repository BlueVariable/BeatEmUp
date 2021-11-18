using UnityEngine;
using BLUE.FSMSystem;
using BLUE.Helpers;
using BLUE.InputSystem;

namespace BLUE.ProjectX.Character.States
{
    // TODO : anticipation duration must be normalized according the animation time
    // TODO : both anticipation & recovery states can be make common 
    public class State_CharacterAttack_PreMeleeAttack : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private FSM_CharacterAttack _attackFSM = null;
        
        [SerializeField] private float _anticipationDuration = 0.35f;

        [SerializeField] private int _targetMouseButton = 0;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.PreMeleeAttack;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            // Convert this to animation normalized duration event
            CoroutineHelper.Instance.WaitForSeconds(_anticipationDuration, () =>
            {
                _attackFSM.TryChangeState(ECharacterAttackState.MeleeAttack);
            });
            
            base.EnterStateCustomActions(transitionMessage);
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
            if (inputData.MouseButton != _targetMouseButton)
            {
                return;
            }

            _attackFSM.TryChangeState(ECharacterAttackState.PreMeleeAttack);
        }

        #endregion
    }
}