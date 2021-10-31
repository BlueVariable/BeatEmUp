using UnityEngine;
using BLUE.FSMSystem;
using BLUE.InputSystem;
using BLUE.ProjectX.Character.Platform;
using BLUE.ProjectX.Common.Movement;
using Character.Animation;

namespace BLUE.ProjectX.Character.Movement
{
    public class State_CharacterMovement_Move : StateBase<ECharacterMovementState, FSMTransitionMessage_CharacterMovement>
    {
        private const string _MOVEMENT_SPEED_PARAM_NAME = "MOVEMENT_SPEED";
        
        [SerializeField] private MovementBehaviour_CharacterController _movementBehaviour = null;

        [SerializeField] private FSM_CharacterPlatform _platformFSM = null;
        
        [SerializeField] private CharacterAnimationController _animationController = null;
        
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
            InputManager.Instance.TryRegisterToInputListener<InputData_HorizontalAxis>(
                EInputEvent.HorizontalAxis,
                OnInputPerformedInMoveState);

            TryPlayAnimation();
            
            _animationController.UpdateParameter(_MOVEMENT_SPEED_PARAM_NAME, 0);
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            InputManager.Instance.TryUnregisterFromInputListener<InputData_HorizontalAxis>(
                EInputEvent.HorizontalAxis,
                OnInputPerformedInMoveState);
            
            base.ExitStateCustomActions();
        }

        private void OnInputPerformedInMoveState(InputData_HorizontalAxis inputData)
        {
            if (inputData.InputState != EHorizontalAxisInputState.Continuing)
            {
                return;
            }
            
            Vector3 movementSpeed = _MovementSpeedProvider.GetMovementSpeed() * inputData.Amount;
                
            _movementBehaviour.Move(movementSpeed);
            
            _animationController.UpdateParameter(_MOVEMENT_SPEED_PARAM_NAME, Mathf.Abs(inputData.Amount));
        }

        private void TryPlayAnimation()
        {
            if (_platformFSM.TryGetCurStateType(out ECharacterPlatformState platformState))
            {
                if (platformState == ECharacterPlatformState.Jump ||
                    platformState == ECharacterPlatformState.DoubleJump)
                {
                    _animationController.TryCrossFadeAnimation(ECharacterAnimation.JUMP_LOOP, 0.25f, 0, false);
                    
                    return;
                }
            }
            
            _animationController.TryGetCurrentAnimType(0, out ECharacterAnimation animType);
            
            if (animType == ECharacterAnimation.JUMP_LOOP)
            {
                return;
            }
            
            _animationController.TryCrossFadeAnimation(ECharacterAnimation.MOVE, 0.25f, 0, false);
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
            if (inputData.InputState == EHorizontalAxisInputState.Started || inputData.InputState == EHorizontalAxisInputState.Continuing)
            {
                _CharacterMovementFSM.TryChangeState(ECharacterMovementState.Move);
            }

            if (inputData.InputState == EHorizontalAxisInputState.Finished)
            {
                _CharacterMovementFSM.TryChangeState(ECharacterMovementState.Idle);
            }
        }

        #endregion
    }
}
