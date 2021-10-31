using UnityEngine;
using BLUE.FSMSystem;
using BLUE.Helpers;
using BLUE.InputSystem;
using BLUE.ProjectX.Common.JumpScripts;
using BLUE.ProjectX.Common.Physics;
using Character.Animation;

namespace BLUE.ProjectX.Character.Platform
{
    public class State_CharacterPlatform_Jump : StateBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform>
    {
        [SerializeField] private JumpBehaviour_CharacterController _jumpBehaviour = null;

        [SerializeField] private CharacterAnimationController _animationController = null;
        
        [SerializeField] private KeyCode _jumpTriggerKeycode = KeyCode.Space;

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
            return ECharacterPlatformState.Jump;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterPlatform transitionMessage)
        {
            _jumpBehaviour.OnLanded += OnLanded;
        
            _jumpBehaviour.Jump();

            _animationController.TryCrossFadeAnimation(ECharacterAnimation.JUMP_LOOP, 0.25f, 0, false);
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _jumpBehaviour.OnLanded -= OnLanded;
            
            _jumpBehaviour.CancelJump();
            
            base.ExitStateCustomActions();
        }

        private void OnLanded()
        {
            _PlatformFSM.TryChangeState(ECharacterPlatformState.Idle);
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

            _PlatformFSM.TryChangeState(ECharacterPlatformState.Jump);
        }

        #endregion
    }
}
