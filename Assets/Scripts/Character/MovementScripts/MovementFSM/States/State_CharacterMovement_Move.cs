using UnityEngine;
using BLUE.FSMSystem;
using BLUE.InputSystem;
using BLUE.ProjectX.Common.Movement;

namespace BLUE.ProjectX.Character.Movement
{
    [RequireComponent(typeof(IMovementSpeedProvider))]
    public class State_CharacterMovement_Move : StateBase<EState, FSMTransitionMessage_CharacterMovement, FSMTransition_CharacterMovement>
    {
        [SerializeField] private MovementBehaviour_CharacterController _movementBehaviour = null;

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
        
        private FSM_CharacterMovement _fsmCharacterMovement;

        private FSM_CharacterMovement FsmCharacterMovement
        {
            get
            {
                if (_fsmCharacterMovement == null)
                {
                    _fsmCharacterMovement = GetComponentInParent<FSM_CharacterMovement>();
                }

                return _fsmCharacterMovement;
            }
        }
        
        public override EState GetStateType()
        {
            return EState.Move;
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
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryRegisterToInputListener<InputData_HorizontalAxis>(
                EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformed);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_HorizontalAxis>(
                EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformed);
        }

        private void OnHorizontalInputPerformed(InputData_HorizontalAxis inputData)
        {
            if (inputData.InputState == EHorizontalAxisInputState.Started)
            {
                FsmCharacterMovement.TryChangeState(EState.Move);
            }

            if (inputData.InputState == EHorizontalAxisInputState.Finished)
            {
                FsmCharacterMovement.TryChangeState(EState.Idle);
            }

            _MovementFSM.TryGetCurStateType(out EState curState);
            
            if (curState == GetStateType())
            {
                Vector3 movementSpeed = _MovementSpeedProvider.GetMovementSpeed() * inputData.Amount;
                
                _movementBehaviour.Move(movementSpeed);
            }
        }

        #endregion
    }
}
