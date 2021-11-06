using UnityEngine;
using BLUE.FSMSystem;
using BLUE.Helpers;
using BLUE.InputSystem;
using BLUE.ProjectX.Common;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterPlatform_DoubleJump : StateBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform>
    {
        [SerializeField] private JumpBehaviour_CharacterController _jumpBehaviour = null;
        
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

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterPlatform transitionMessage)
        {
            _jumpBehaviour.OnJumpFinished += OnLanded;
            
            _jumpBehaviour.DoubleJumpBoost();
            
            _jumpBehaviour.StartJump();
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _jumpBehaviour.OnJumpFinished -= OnLanded;
            
            _jumpBehaviour.StopJump();
            
            base.ExitStateCustomActions();
        }

        private void OnLanded()
        {
            _PlatformFSM.TryChangeState(ECharacterPlatformState.Idle);
        }

        #region Input Related

        private void Awake()
        {
            RegisterToSelfFSM();
        }

        private void OnDestroy()
        {
            UnregisterFromSelfFSM();
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
            if (platformState != ECharacterPlatformState.Jump)
            {
                return;
            }

            _delayRoutineID = CoroutineHelper.Instance.WaitForSeconds(_doubleJumpTriggerDelay, RegisterToInputManager);
        }
        
        private void OnExitedState(ECharacterPlatformState platformState)
        {
            if (platformState != ECharacterPlatformState.Jump)
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

        #endregion
    }
}