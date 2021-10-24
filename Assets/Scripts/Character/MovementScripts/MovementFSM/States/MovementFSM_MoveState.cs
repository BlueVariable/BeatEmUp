using UnityEngine;
using BLUE.FSMSystem;
using BLUE.InputSystem;
using BLUE.ProjectX.Common.Movement;

namespace BLUE.ProjectX.Character.Movement
{
    [RequireComponent(typeof(IMovementSpeedProvider))]
    public class MovementFSM_MoveState : StateBase<EState, MovementFSMTransitionMessage, MovementFSMTransition>
    {
        [SerializeField] private MovementBehaviour_CharacterController _movementBehaviour = null;
        
        // TODO :: Requires refactoring ( Requested extended fsm features )
        private bool _isStateActive = false;
        
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
        
        private MovementFSM _movementFSM;

        private MovementFSM _MovementFSM
        {
            get
            {
                if (_movementFSM == null)
                {
                    _movementFSM = GetComponentInParent<MovementFSM>();
                }

                return _movementFSM;
            }
        }
        
        public override EState GetStateType()
        {
            return EState.Move;
        }

        protected override void EnterStateCustomActions(MovementFSMTransitionMessage transitionMessage)
        {
            _isStateActive = true;
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _isStateActive = false;
            
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
                _MovementFSM.TryChangeState(EState.Move);
            }

            if (inputData.InputState == EHorizontalAxisInputState.Finished)
            {
                _MovementFSM.TryChangeState(EState.Idle);
            }

            if (_isStateActive)
            {
                Vector3 movementSpeed = _MovementSpeedProvider.GetMovementSpeed() * inputData.Amount;
                
                _movementBehaviour.Move(movementSpeed);
            }
        } 

        #endregion
    }
}