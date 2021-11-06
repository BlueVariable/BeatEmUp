using BLUE.FSMSystem;
using BLUE.InputSystem;
using BLUE.ProjectX.Common;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterPlatform_Jump : StateBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform>
    {
        [SerializeField] private JumpBehaviour_CharacterController _jumpBehaviour = null;

        [SerializeField] private AnimationController_Character _animationController = null;
        
        [SerializeField] private KeyCode _jumpTriggerKeycode = KeyCode.Space;

        [SerializeField] private float _fixedTransitionDuration = 0.25f;
        
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
            _jumpBehaviour.OnJumpFinished += OnLanded;
        
            _jumpBehaviour.StartJump();

            _animationController.TryCrossFadeAnimation(ECharacterAnimation.JUMP_LOOP, 
                _fixedTransitionDuration, 0, false);
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _jumpBehaviour.OnJumpFinished -= OnLanded;
            
            _jumpBehaviour.StopJump();
            
            base.ExitStateCustomActions();
        }
        
        private void Awake()
        {
            RegisterToInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }

        private void OnLanded()
        {
            _PlatformFSM.TryChangeState(ECharacterPlatformState.Idle);
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
            if (!inputData.KeyCode.Equals(_jumpTriggerKeycode))
            {
                return;
            }

            _PlatformFSM.TryChangeState(ECharacterPlatformState.Jump);
        }

        #endregion
    }
}
