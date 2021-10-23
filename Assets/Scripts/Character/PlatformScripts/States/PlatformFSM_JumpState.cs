using UnityEngine;
using BLUE.FSMSystem;
using BLUE.InputSystem;
using BLUE.ProjectX.Common.JumpScripts;

namespace BLUE.ProjectX.Character.Platform.States
{
    public class PlatformFSM_JumpState : StateBase<EState, PlatformFSMTransitionMessage>
    {
        [SerializeField] private PlatformFSM _platformFsm = null;
        
        [SerializeField] private JumpBehaviour_CharacterController _jumpBehaviour = null;

        [SerializeField] private KeyCode _jumpTriggerKeycode = KeyCode.Space;
        
        public override EState GetStateType()
        {
            return EState.Jump;
        }

        protected override void EnterStateCustomActions(PlatformFSMTransitionMessage transitionMessage)
        {
            _jumpBehaviour.OnLanded += OnLanded;
            
            _jumpBehaviour.Jump();
            
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
            if (inputData.KeyCode != _jumpTriggerKeycode)
            {
                return;
            }

            _platformFsm.TryChangeState(EState.Jump);
        }

        #endregion
    }
}
