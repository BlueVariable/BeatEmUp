using System;
using UnityEngine;
using BLUE.FSMSystem;
using BLUE.InputSystem;
using BLUE.ProjectX.Common;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterMovement_Dash : StateBase<ECharacterMovementState, FSMTransitionMessage_CharacterMovement>
    {
        [SerializeField] private DashBehaviourBase _dashBehaviour = null;

        [SerializeField] private JumpBehaviourBase _jumpBehaviour = null;
        
        [SerializeField] private GravityApplierBase _gravityApplier = null;

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

        private DashConditionBase[] _conditions;
        private DashConditionBase[] _Conditions
        {
            get
            {
                if (_conditions == null)
                {
                    _conditions = GetComponentsInChildren<DashConditionBase>();
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

            _dashBehaviour.OnDashFinished += OnDashFinished;

            _gravityApplier.ToggleGravity(false);

            _jumpBehaviour.ResetJumpForceToGravity();

            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _gravityApplier.ToggleGravity(true);
            
            base.ExitStateCustomActions();
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
            if (!inputData.KeyCode.Equals(_dashTriggerKey))
            {
                return;
            }

            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(
                EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformed);
        }

        private void OnKeyUp(InputData_Key inputData)
        {
            if (!inputData.KeyCode.Equals(_dashTriggerKey))
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(
                EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformed);
        }

        private void OnHorizontalInputPerformed(InputData_Axis inputData)
        {
            if (Mathf.Abs(inputData.Amount) < _dashThreshold)
            {
                return;
            }

            foreach (DashConditionBase condition in _Conditions)
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
        
        private void OnDashFinished()
        {
            _dashBehaviour.OnDashFinished -= OnDashFinished;
            
            _MovementFSM.TryChangeState(ECharacterMovementState.Idle);
        }
    }
}
