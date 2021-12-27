using BLUE.FSMSystem;
using BLUE.InputSystem;
using BLUE.ProjectX.Common;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterMovement_Move : StateBase<ECharacterMovementState, FSMTransitionMessage_CharacterMovement>
    {
        #region Constants

        private const string MOVEMENT_SPEED_PARAM_NAME = "MOVEMENT_SPEED";

        #endregion
        
        [SerializeField] private MovementBehaviourBase _movementBehaviour = null;
        
        [SerializeField] private AnimationController_Character _characterAnimationController = null;

        [SerializeField] private RotationBehaviourBase _rotationBehaviour = null;
        
        private Vector2 _lastExecutedInput = Vector2.zero;
        
        private IMovementSpeedProvider _movementSpeedProvider;
        private IMovementSpeedProvider _MovementSpeedProvider
        {
            get
            {
                if (_movementSpeedProvider == null)
                {
                    _movementSpeedProvider = GetComponent<IMovementSpeedProvider>();
                }

                return _movementSpeedProvider;
            }
        } 
        
        private FSM_CharacterMovement _characterMovementFSM;
        private FSM_CharacterMovement _CharacterMovementFSM
        {
            get
            {
                if (_characterMovementFSM == null)
                {
                    _characterMovementFSM = GetComponentInParent<FSM_CharacterMovement>();
                }

                return _characterMovementFSM;
            }
        }
        
        public override ECharacterMovementState GetStateType()
        {
            return ECharacterMovementState.Move;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterMovement transitionMessage)
        {
            RegisterToInStateInputManager();

            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            UnregisterFromInStateInputManager();
            
            _lastExecutedInput = Vector2.zero;

            base.ExitStateCustomActions();
        }
        
        private void Awake()
        {
            RegisterToInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();

            UnregisterFromInStateInputManager();
        }

        private void RegisterToInStateInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(
                EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformedInMoveState);
            
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(
                EInputEvent.VerticalAxis,
                OnVerticalInputPerformedInMoveState);
        }

        private void UnregisterFromInStateInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(
                EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformedInMoveState);
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(
                EInputEvent.VerticalAxis,
                OnVerticalInputPerformedInMoveState);
        }

        private void OnHorizontalInputPerformedInMoveState(InputData_Axis inputData)
        {
            _lastExecutedInput.x = inputData.Amount;

            Vector2 normInput = _lastExecutedInput;

            if (_lastExecutedInput.magnitude > 1)
            {
                normInput = _lastExecutedInput.normalized;
            }

            Vector3 movementSpeed = new Vector3(
                _MovementSpeedProvider.GetMovementSpeed().x * normInput.x,
                _MovementSpeedProvider.GetMovementSpeed().y,
                _MovementSpeedProvider.GetMovementSpeed().z * normInput.y);
            
            movementSpeed.z = 0;
            
            Move(movementSpeed);
        }
        
        private void OnVerticalInputPerformedInMoveState(InputData_Axis inputData)
        {
            _lastExecutedInput.y = inputData.Amount;
            
            Vector2 normInput = _lastExecutedInput;

            if (_lastExecutedInput.magnitude > 1)
            {
                normInput = _lastExecutedInput.normalized;
            }

            Vector3 movementSpeed = new Vector3(
                _MovementSpeedProvider.GetMovementSpeed().x * normInput.x,
                _MovementSpeedProvider.GetMovementSpeed().y,
                _MovementSpeedProvider.GetMovementSpeed().z * normInput.y);
            
            movementSpeed.x = 0;
            
            Move(movementSpeed);
        }

        private void Move(Vector3 movementSpeed)
        {
            _movementBehaviour.Move(movementSpeed);
            
            float animSpeed = Mathf.Clamp(Mathf.Abs(_lastExecutedInput.magnitude), 0, 1);
            
            _characterAnimationController.UpdateParameter(MOVEMENT_SPEED_PARAM_NAME, animSpeed);

            Vector3 dir = new Vector3(_lastExecutedInput.x, 0, _lastExecutedInput.y);
            
            if (dir.magnitude > 0)
            {
                Quaternion targetQuaternion = Quaternion.LookRotation(dir, Vector3.up);
                
                _rotationBehaviour.Rotate(targetQuaternion.eulerAngles);
            }
        }

        #region Input Related

        private void RegisterToInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(
                EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformed);
            
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(
                EInputEvent.VerticalAxis,
                OnVerticalInputPerformed);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(
                EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformed);
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(
                EInputEvent.VerticalAxis,
                OnVerticalInputPerformed);
        }

        private void OnHorizontalInputPerformed(InputData_Axis inputData)
        {
            if (inputData.InputState.Equals(EAxisInputState.Started) ||
                inputData.InputState.Equals(EAxisInputState.Continuing))
            {
                _CharacterMovementFSM.TryChangeState(ECharacterMovementState.Move);
            }

            if (inputData.InputState.Equals(EAxisInputState.Finished))
            {
                _lastExecutedInput.x = 0;
                
                if (_lastExecutedInput.magnitude == 0)
                {
                    _CharacterMovementFSM.TryChangeState(ECharacterMovementState.Idle);
                }
            }
        }
        
        private void OnVerticalInputPerformed(InputData_Axis inputData)
        {
            if (inputData.InputState.Equals(EAxisInputState.Started) 
                || inputData.InputState.Equals(EAxisInputState.Continuing))
            {
                _CharacterMovementFSM.TryChangeState(ECharacterMovementState.Move);
            }
            
            if (inputData.InputState.Equals(EAxisInputState.Finished))
            {
                _lastExecutedInput.y = 0;
                
                if (_lastExecutedInput.magnitude == 0)
                {
                    _CharacterMovementFSM.TryChangeState(ECharacterMovementState.Idle);
                }
            }
        }

        #endregion
    }
}
