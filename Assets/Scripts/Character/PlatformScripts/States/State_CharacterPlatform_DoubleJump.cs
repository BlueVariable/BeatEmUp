using UnityEngine;
using BLUE.FSMSystem;
using BLUE.Helpers;
using BLUE.InputSystem;
using BLUE.ProjectX.Common.JumpScripts;
using BLUE.ProjectX.Common.Physics;

namespace BLUE.ProjectX.Character.Platform
{
    public class State_CharacterPlatform_DoubleJump : StateBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform, FSMTransition_CharacterPlatform>
    {
        [SerializeField] private JumpBehaviour_CharacterController _jumpBehaviour = null;
        
        [SerializeField] private KeyCode _doubleJumpTrigger = KeyCode.Space;

        [SerializeField] private float _doubleJumpTriggerDelay = 0.25f;

        [SerializeField] private GravityApplier _gravityApplier = null;

        
        private string _delayRoutineID = string.Empty;
        
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
            _SelfFSM.TryChangeState(ECharacterPlatformState.Idle);
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
        
        private void OnEnterState(ECharacterPlatformState eCharacterPlatformState)
        {
            if (eCharacterPlatformState != ECharacterPlatformState.Jump)
            {
                return;
            }

            _delayRoutineID = CoroutineHelper.Instance.WaitForSeconds(_doubleJumpTriggerDelay, RegisterToInputManager);
        }
        
        private void OnExitState(ECharacterPlatformState eCharacterPlatformState)
        {
            if (eCharacterPlatformState != ECharacterPlatformState.Jump)
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

            _SelfFSM.TryChangeState(ECharacterPlatformState.DoubleJump);
        }

        #endregion
    }
}