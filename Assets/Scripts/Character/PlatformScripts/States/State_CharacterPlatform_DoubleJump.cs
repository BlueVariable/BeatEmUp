using UnityEngine;
using BLUE.FSMSystem;
using BLUE.Helpers;
using BLUE.InputSystem;
using BLUE.ProjectX.Common.JumpScripts;
using BLUE.ProjectX.Common.Physics;

namespace BLUE.ProjectX.Character.Platform
{
    public class PlatformFSM_DoubleJumpState : StateBase<EState, PlatformFSMTransitionMessage>
    {
        [SerializeField] private JumpBehaviour_CharacterController _jumpBehaviour = null;
        
        [SerializeField] private KeyCode _doubleJumpTrigger = KeyCode.Space;

        [SerializeField] private float _doubleJumpTriggerDelay = 0.25f;

        [SerializeField] private GravityApplier _gravityApplier = null;

        private string _delayRoutineID = string.Empty;
        
        private PlatformFSM _platformFSM;
        private PlatformFSM _PlatformFSM
        {
            get
            {
                if (_platformFSM == null)
                {
                    _platformFSM = GetComponentInParent<PlatformFSM>();
                }

                return _platformFSM;
            }
        }
        
        public override EState GetStateType()
        {
            return EState.DoubleJump;
        }

        protected override void EnterStateCustomActions(PlatformFSMTransitionMessage transitionMessage)
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
            _PlatformFSM.TryChangeState(EState.Idle);
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
        
        private void OnEnterState(EState eState)
        {
            if (eState != EState.Jump)
            {
                return;
            }

            _delayRoutineID = CoroutineHelper.Instance.WaitForSeconds(_doubleJumpTriggerDelay, RegisterToInputManager);
        }
        
        private void OnExitState(EState eState)
        {
            if (eState != EState.Jump)
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

            _PlatformFSM.TryChangeState(EState.DoubleJump);
        }

        #endregion
    }
}