using UnityEngine;
using BLUE.FSMSystem;
using BLUE.InputSystem;
using BLUE.ProjectX.Common.JumpScripts;

namespace BLUE.ProjectX.Character.Platform
{
    public class PlatformFSM_DoubleJumpState : StateBase<EState, PlatformFSMTransitionMessage, PlatformFSMTransition>
    {
        [SerializeField] private JumpBehaviour_CharacterController _jumpBehaviour = null;
        
        [SerializeField] private KeyCode _doubleJumpTrigger = KeyCode.Space;

        [SerializeField] private PlatformFSM _platformFsm = null;
        
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
            _platformFsm.TryChangeState(EState.Idle);
        }

        private void Awake()
        {
            RegisterToInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }

        #region Input Related

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
            if (inputData.KeyCode != _doubleJumpTrigger)
            {
                return;
            }

            _platformFsm.TryChangeState(EState.DoubleJump);
        }

        #endregion
    }
}