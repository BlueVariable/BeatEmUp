using System.Collections;
using BLUE.FSMSystem;
using BLUE.Helpers;
using BLUE.InputSystem;
using BLUE.ProjectX.ClimbSystem;
using BLUE.ProjectX.Common.Physics;
using UnityEngine;

namespace BLUE.ProjectX.Character.Platform
{
    public class State_CharacterPlatform_Climb : StateBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform>
    {
        [SerializeField] private Climber _climber = null;

        [SerializeField] private GravityApplier _gravityApplier = null;
        
        [SerializeField] private KeyCode _climbTrigger = KeyCode.LeftShift;

        [SerializeField] private float _climbCooldown = 0;

        private bool _canEnterClimbStates = true;
        
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
            return ECharacterPlatformState.Climb;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterPlatform transitionMessage)
        {
            _canEnterClimbStates = false;
            
            _gravityApplier.ToggleGravity(false);

            StartClimbableControlRoutine();
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _gravityApplier.ToggleGravity(true);
            
            StopClimbableControlRoutine();

            CoroutineHelper.Instance.WaitForSeconds(_climbCooldown, () =>
            {
                _canEnterClimbStates = true;
            });
            
            base.ExitStateCustomActions();
        }

        #region Climbable Control

        private IEnumerator _climbableControlRoutine;
        
        private void StartClimbableControlRoutine()
        {
            StopClimbableControlRoutine();

            _climbableControlRoutine = ClimbableControlProgress();

            StartCoroutine(_climbableControlRoutine);
        }

        private void StopClimbableControlRoutine()
        {
            if (_climbableControlRoutine != null)
            {
                StopCoroutine(_climbableControlRoutine);
            }
        }

        private IEnumerator ClimbableControlProgress()
        {
            while (true)
            {
                if (!_climber.CheckCanHoldClimbable())
                {
                    _PlatformFSM.TryChangeState(ECharacterPlatformState.Idle);
                }
                
                yield return null;
            }
        }

        #endregion

        #region Input Related

        private void Awake()
        {
            RegisterToInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null) 
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void OnKeyDown(InputData_Key inputData)
        {
            if (inputData.KeyCode != _climbTrigger)
            {
                return;
            }
            
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyHold, OnKeyHold);
        }

        private void OnKeyUp(InputData_Key inputData)
        {
            if (inputData.KeyCode != _climbTrigger)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyHold, OnKeyHold);

            _PlatformFSM.TryGetCurStateType(out ECharacterPlatformState curState);

            if (curState == GetStateType())
            {
                _PlatformFSM.TryChangeState(ECharacterPlatformState.Idle);
            }
        }
        
        private void OnKeyHold(InputData_Key inputData)
        {
            if (inputData.KeyCode != _climbTrigger)
            {
                return;
            }

            if (!_canEnterClimbStates)
            {
                return;
            }

            _PlatformFSM.TryGetCurStateType(out ECharacterPlatformState curState);

            if (curState == GetStateType())
            {
                return;
            }
            
            if (_climber.CheckCanHoldClimbable())
            {
                _PlatformFSM.TryChangeState(GetStateType());
            }
        }

        #endregion
    }
}