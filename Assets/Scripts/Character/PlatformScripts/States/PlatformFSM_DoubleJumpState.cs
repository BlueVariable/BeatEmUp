using UnityEngine;
using BLUE.FSMSystem;
using BLUE.Helpers;
using BLUE.InputSystem;
using BLUE.ProjectX.Common.JumpScripts;

namespace BLUE.ProjectX.Character.Platform
{
    public class PlatformFSM_DoubleJumpState : StateBase<EState, PlatformFSMTransitionMessage, PlatformFSMTransition>
    {
        [SerializeField] private JumpBehaviour_CharacterController _jumpBehaviour = null;
        
        [SerializeField] private KeyCode _doubleJumpTrigger = KeyCode.Space;

        [SerializeField] private float _doubleJumpTriggerDelay = 0.25f;
        
        public override EState GetStateType()
        {
            return EState.DoubleJump;
        }

        protected override void EnterStateCustomActions(PlatformFSMTransitionMessage transitionMessage)
        {
            _jumpBehaviour.OnLanded += OnLanded;
            
            _jumpBehaviour.DoubleJumpBoost();
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _jumpBehaviour.OnLanded -= OnLanded;
            
            base.ExitStateCustomActions();
        }

        private void OnLanded()
        {
            _SelfFSM.TryChangeState(EState.Idle);
        }

        private void Awake()
        {
            RegisterToSelfFSM();
        }

        private void OnDestroy()
        {
            UnregisterFromSelfFSM();
        }

        #region Input Related

        private void RegisterToSelfFSM()
        {
            _SelfFSM.OnStateEntered += OnEnterState;
            _SelfFSM.OnStateExited += OnExitState;
        }

        private void UnregisterFromSelfFSM()
        {
            _SelfFSM.OnStateEntered -= OnEnterState;
            _SelfFSM.OnStateExited -= OnExitState;
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

            CoroutineHelper.Instance.WaitForSeconds(_doubleJumpTriggerDelay, RegisterToInputManager);
        }
        
        private void OnExitState(EState eState)
        {
            if (eState != EState.Jump)
            {
                return;
            }
            
            UnregisterFromInputManager();
        }
        
        private void OnKeyUp(InputData_Key inputData)
        {
            if (inputData.KeyCode != _doubleJumpTrigger)
            {
                return;
            }

            _SelfFSM.TryChangeState(EState.DoubleJump);
        }

        #endregion
    }
}