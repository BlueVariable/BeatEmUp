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

        [SerializeField] private Transform _characterRoot = null;

        [SerializeField] private AnimationController_Character _characterAnimationController = null;

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
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(
                EInputEvent.HorizontalAxis,
                OnInputPerformedInMoveState);

            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(
                EInputEvent.HorizontalAxis,
                OnInputPerformedInMoveState);
            
            base.ExitStateCustomActions();
        }
        
        private void Awake()
        {
            RegisterToInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }

        private void OnInputPerformedInMoveState(InputData_Axis inputData)
        {
            if (!inputData.InputState.Equals(EAxisInputState.Continuing))
            {
                return;
            }
            
            Vector3 movementSpeed = _MovementSpeedProvider.GetMovementSpeed() * inputData.Amount;
                
            _movementBehaviour.Move(movementSpeed);
            
            _characterRoot.rotation = Quaternion.Euler(0,90 * Mathf.Sign(inputData.Amount), 0);
            
            _characterAnimationController.UpdateParameter(MOVEMENT_SPEED_PARAM_NAME, Mathf.Abs(inputData.Amount));
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
        }

        private void OnHorizontalInputPerformed(InputData_Axis inputData)
        {
            if (inputData.InputState.Equals(EAxisInputState.Started) 
                || inputData.InputState.Equals(EAxisInputState.Continuing))
            {
                _CharacterMovementFSM.TryChangeState(ECharacterMovementState.Move);
            }

            if (inputData.InputState.Equals(EAxisInputState.Finished))
            {
                _CharacterMovementFSM.TryChangeState(ECharacterMovementState.Idle);
            }
        }

        #endregion
    }
}
