using UnityEngine;
using BLUE.FSMSystem;
using BLUE.Helpers;
using BLUE.InputSystem;
using BLUE.ProjectX.Common;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterPlatform_DoubleJump : StateBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform>
    {
        [SerializeField] private FSM_CharacterMovement _movementFSM = null;
        
        [SerializeField] private JumpBehaviourBase _jumpBehaviour = null;
        
        [SerializeField] private float _doubleJumpTriggerDelay = 0.25f;
        
        [SerializeField] private KeyCode _doubleJumpTrigger = KeyCode.Space;

        private string _delayRoutineID = string.Empty;
        
        private FSM_CharacterPlatform _platformFSM;
        private FSM_CharacterPlatform _PlatformFSM
        {
            get
            {
                if (_platformFSM == null)
                {
                    _platformFSM = GetComponentInParent<FSM_CharacterPlatform>();
                }

                return _platformFSM;
            }
        }
        
        public override ECharacterPlatformState GetStateType()
        {
            return ECharacterPlatformState.DoubleJump;
        }

        protected override bool CanEnterStateCustomActions(FSMTransitionMessage_CharacterPlatform transitionMessage)
        {
            _movementFSM.TryGetCurStateType(out ECharacterMovementState curMovementState);

            return curMovementState != ECharacterMovementState.Dash;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterPlatform transitionMessage)
        {
            RegisterToJumpBehaviour();
            
            _jumpBehaviour.AddDoubleJumpBoost();
            
            _jumpBehaviour.StartJump();
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            UnregisterFromJumpBehaviour();
            
            _jumpBehaviour.StopJump();
            
            base.ExitStateCustomActions();
        }
        
        private void Awake()
        {
            RegisterToSelfFSM();
        }

        private void OnDestroy()
        {
            UnregisterFromSelfFSM();
            
            UnregisterFromInputManager();
            
            UnregisterFromJumpBehaviour();
        }

        private void RegisterToJumpBehaviour()
        {
            _jumpBehaviour.OnJumpFinished += OnJumpFinished;
        }

        private void UnregisterFromJumpBehaviour()
        {
            _jumpBehaviour.OnJumpFinished -= OnJumpFinished;
        }

        private void OnJumpFinished()
        {
            _PlatformFSM.TryChangeState(ECharacterPlatformState.Idle);
        }

        private void RegisterToSelfFSM()
        {
            _PlatformFSM.OnStateEntered += OnEnteredState;
            _PlatformFSM.OnStateExited += OnExitedState;
        }

        private void UnregisterFromSelfFSM()
        {
            _PlatformFSM.OnStateEntered -= OnEnteredState;
            _PlatformFSM.OnStateExited -= OnExitedState;
        }

        private void OnEnteredState(ECharacterPlatformState platformState)
        {
            if (!platformState.Equals(ECharacterPlatformState.Jump))
            {
                return;
            }

            _delayRoutineID = CoroutineHelper.Instance.WaitForSeconds(_doubleJumpTriggerDelay, RegisterToInputManager);
        }
        
        private void OnExitedState(ECharacterPlatformState platformState)
        {
            if (!platformState.Equals(ECharacterPlatformState.Jump))
            {
                return;
            }

            CoroutineHelper.Instance.TryCancelCallback(_delayRoutineID);
            
            UnregisterFromInputManager();
        }
        
        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void UnregisterFromInputManager()
        {
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }
        
        private void OnKeyUp(InputData_Key inputData)
        {
            if (!inputData.KeyCode.Equals(_doubleJumpTrigger))
            {
                return;
            }

            _PlatformFSM.TryChangeState(ECharacterPlatformState.DoubleJump);
        }
    }
}