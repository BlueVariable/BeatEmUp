using BLUE.FSMSystem;
using BLUE.Helpers;
using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character.States
{
    public class State_CharacterAttack_PreRangedAttack : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private FSM_CharacterAttack _attackFSM = null;
        
        [SerializeField] private float _anticipationDuration = 0.35f;

        [SerializeField] private int _targetMouseButton = 1;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.PreRangedAttack;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            
            CoroutineHelper.Instance.WaitForSeconds(_anticipationDuration, () =>
            {
                _attackFSM.TryChangeState(ECharacterAttackState.RangedAttack);
            });
            
            base.EnterStateCustomActions(transitionMessage);
        }
        
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

            _attackFSM.TryChangeState(ECharacterAttackState.PreRangedAttack);
        }
    }
}