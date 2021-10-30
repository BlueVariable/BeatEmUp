using System;
using BLUE.FSMSystem;
using BLUE.InputSystem;
using UnityEngine;
using BLUE.ProjectX.Common.DashScripts;

namespace BLUE.ProjectX.Character.Movement
{
    public class State_CharacterMovement_Dash : StateBase<ECharacterMovementState, FSMTransitionMessage_CharacterMovement>
    {
        [SerializeField] private DashBehaviour _dashBehaviour = null;
        
        [SerializeField] private KeyCode _dashTriggerKey = KeyCode.None;

        [SerializeField] private float _dashThreshold = 0.5f;
        
        private int _dashDirection = 0;
        
        private FSM_CharacterMovement _movementFSM;
        private FSM_CharacterMovement _MovementFSM
        {
            get
            {
                if (_movementFSM == null)
                {
                    _movementFSM = GetComponentInParent<FSM_CharacterMovement>();
                }

                return _movementFSM;
            }
        }


        private DashCondition[] _conditions;

        private DashCondition[] _Conditions
        {
            get
            {
                if (_conditions == null)
                {
                    _conditions = GetComponentsInChildren<DashCondition>();
                }

                return _conditions;
            }
        }
        
        public override ECharacterMovementState GetStateType()
        {
            return ECharacterMovementState.Dash;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterMovement transitionMessage)
        {
            _dashBehaviour.Dash(_dashDirection);

            _dashBehaviour.OnDashCompleted += OnDashCompleted;
            
            base.EnterStateCustomActions(transitionMessage);
        }

        private void OnDashCompleted()
        {
            _dashBehaviour.OnDashCompleted -= OnDashCompleted;
            
            _MovementFSM.TryChangeState(ECharacterMovementState.Idle);
        }

        private void Awake()
        {
            RegisterToInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }
        
        #region Input Related

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void OnKeyDown(InputData_Key inputData)
        {
            if (inputData.KeyCode != _dashTriggerKey)
            {
                return;
            }

            InputManager.Instance.TryRegisterToInputListener<InputData_HorizontalAxis>(
                EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformed);
        }

        private void OnKeyUp(InputData_Key inputData)
        {
            if (inputData.KeyCode != _dashTriggerKey)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_HorizontalAxis>(
                EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformed);
        }

        private void OnHorizontalInputPerformed(InputData_HorizontalAxis inputData)
        {
            if (Mathf.Abs(inputData.Amount) < _dashThreshold)
            {
                return;
            }

            foreach (DashCondition condition in _Conditions)
            {
                if (!condition.CanPerformDash())
                {
                    return;
                }
            }
            
            _dashDirection = Math.Sign(inputData.Amount);

            _MovementFSM.TryChangeState(ECharacterMovementState.Dash);
        }

        #endregion
    }
}
