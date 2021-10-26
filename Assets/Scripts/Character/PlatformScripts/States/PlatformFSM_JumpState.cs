using UnityEngine;
using BLUE.FSMSystem;
using BLUE.InputSystem;
using BLUE.ProjectX.Common.JumpScripts;
using BLUE.ProjectX.Common.Physics;

namespace BLUE.ProjectX.Character.Platform
{
    public class PlatformFSM_JumpState : StateBase<EState, PlatformFSMTransitionMessage, PlatformFSMTransition>
    {
        [SerializeField] private JumpBehaviour_CharacterController _jumpBehaviour = null;

        [SerializeField] private KeyCode _jumpTriggerKeycode = KeyCode.Space;

        [SerializeField] private GravityApplier _gravityApplier = null;
        
        public override EState GetStateType()
        {
            return EState.Jump;
        }

        protected override void EnterStateCustomActions(PlatformFSMTransitionMessage transitionMessage)
        {
            _gravityApplier.ToggleGravity(false);
            
            _jumpBehaviour.OnLanded += OnLanded;
            
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
            _SelfFSM.TryChangeState(EState.Idle);
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
            if (InputManager.Instance != null)
            {
                InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
            }
        }

        private void OnKeyUp(InputData_Key inputData)
        {
            if (inputData.KeyCode != _jumpTriggerKeycode)
            {
                return;
            }

            _SelfFSM.TryChangeState(EState.Jump);
        }

        #endregion
    }
}
