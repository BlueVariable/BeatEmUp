using UnityEngine;
using BLUE.FSMSystem;
using BLUE.Helpers;
using BLUE.InputSystem;
using BLUE.ProjectX.Common.JumpScripts;
using BLUE.ProjectX.Common.Physics;

namespace BLUE.ProjectX.Character.Platform
{
    public class State_CharacterPlatform_DoubleJump : StateBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform>
    {
        [SerializeField] private JumpBehaviour_CharacterController _jumpBehaviour = null;
        
        [SerializeField] private KeyCode _doubleJumpTrigger = KeyCode.Space;

        [SerializeField] private float _doubleJumpTriggerDelay = 0.25f;

        [SerializeField] private GravityApplier _gravityApplier = null;

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
            _gravityApplier.ToggleGravity(false);
            
            _jumpBehaviour.OnLanded += OnLanded;
            
            _jumpBehaviour.DoubleJumpBoost();
            
            _jumpBehaviour.Jump();
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _gravityApplier.ToggleGravity(true);
            
            _jumpBehaviour.OnLanded -= OnLanded;
            
            _jumpBehaviour.CancelJump();
            
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
            _PlatformFSM.OnStateEntered += OnEnterState;
            _PlatformFSM.OnStateExited += OnExitState;
        }

        private void UnregisterFromSelfFSM()
        {
            _PlatformFSM.OnStateEntered -= OnEnterState;
            _PlatformFSM.OnStateExited -= OnExitState;
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void UnregisterFromInputManager()
        {
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }
        
        private void OnEnterState(ECharacterPlatformState eState)
        {
            if (eState != ECharacterPlatformState.Jump)
            {
                return;
            }

            _delayRoutineID = CoroutineHelper.Instance.WaitForSeconds(_doubleJumpTriggerDelay, RegisterToInputManager);
        }
        
        private void OnExitState(ECharacterPlatformState eState)
        {
            if (eState != ECharacterPlatformState.Jump)
            {
                return;
            }

            CoroutineHelper.Instance.TryCancelCallback(_delayRoutineID);
            
            UnregisterFromInputManager();
        }
        
        private void OnKeyUp(InputData_Key inputData)
        {
            if (inputData.KeyCode != _doubleJumpTrigger)
            {
                return;
            }

            _PlatformFSM.TryChangeState(ECharacterPlatformState.DoubleJump);
        }

        #endregion
    }
}