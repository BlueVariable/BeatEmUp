using System.Collections;
using BLUE.FSMSystem;
using BLUE.InputSystem;
using BLUE.ProjectX.ClimbSystem;
using BLUE.ProjectX.Common.Physics;
using UnityEngine;

namespace BLUE.ProjectX.Character.Platform
{
    public class PlatfromFSM_ClimbState : StateBase<EState, PlatformFSMTransitionMessage, PlatformFSMTransition>
    {
        [SerializeField] private Climber _climber = null;

        [SerializeField] private GravityApplier _gravityApplier = null;
        
        [SerializeField] private KeyCode _climbTrigger = KeyCode.LeftShift;
        
        public override EState GetStateType()
        {
            return EState.Climb;
        }

        protected override void EnterStateCustomActions(PlatformFSMTransitionMessage transitionMessage)
        {
            _gravityApplier.ToggleGravity(false);

            StartClimbableControlRoutine();
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _gravityApplier.ToggleGravity(true);
            
            StopClimbableControlRoutine();
            
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
                    _SelfFSM.TryChangeState(EState.Idle);
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

            _SelfFSM.TryGetCurStateType(out EState curState);

            if (curState == GetStateType())
            {
                _SelfFSM.TryChangeState(EState.Idle);
            }
        }
        
        private void OnKeyHold(InputData_Key inputData)
        {
            if (inputData.KeyCode != _climbTrigger)
            {
                return;
            }

            _SelfFSM.TryGetCurStateType(out EState curState);

            if (curState == GetStateType())
            {
                return;
            }
            
            if (_climber.CheckCanHoldClimbable())
            {
                _SelfFSM.TryChangeState(GetStateType());
            }
        }

        #endregion
    }
}