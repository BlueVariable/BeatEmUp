using System;
using UnityEngine;
using System.Collections;

namespace BLUE.ProjectX.Common
{
    public class JumpBehaviour_MovementBehaviourBased : JumpBehaviourBase
    {
        [SerializeField] private MovementBehaviourBase _movementBehaviour = null;
        
        [SerializeField] private CharacterController _characterController = null;

        [SerializeField] private GravityApplierBase _gravityApplier = null;
        
        [SerializeField] private float _jumpForce = 10f;

        [SerializeField] private float _jumpGravityCoef = 1f;
        
        [SerializeField] private float _fallOfGravityCoef = 1f;

        private float _curJumpForce = 0;

        private IEnumerator _jumpRoutine;

        #region Events

        public override Action OnJumpStarted { get; set; }
        
        public override Action OnJumpFinished { get; set; }

        #endregion

        public override void StartJump()
        {
            StartJumpRoutine();
        }

        public override void AddDoubleJumpBoost()
        {
            _curJumpForce = _jumpForce + Mathf.Abs(_gravityApplier.Gravity.y);
        }

        public override void StopJump()
        {
            StopJumpRoutine();
        }

        public override void ResetJumpForceToGravity()
        {
            _curJumpForce = Mathf.Abs(_gravityApplier.Gravity.y);
        }

        private void StartJumpRoutine()
        {
            StopJumpRoutine();

            _jumpRoutine = JumpProgress();

            StartCoroutine(_jumpRoutine);
        }

        private void StopJumpRoutine()
        {
            if (_jumpRoutine != null)
            {
                StopCoroutine(_jumpRoutine);
            }
        }

        private IEnumerator JumpProgress()
        {
            OnJumpStarted?.Invoke();

            _curJumpForce = _jumpForce + Mathf.Abs(_gravityApplier.Gravity.y);

            float timePassed = 0;

            while (!_characterController.isGrounded || timePassed < 0.25f)
            {
                if (_gravityApplier.IsGravityBlocked)
                {
                    yield return null;
                    
                    continue;
                }
                
                Vector3 jumpForce = new Vector3(0, _curJumpForce, 0);

                float deltaForce = 0;
                
                if (_curJumpForce > Mathf.Abs(_gravityApplier.Gravity.y))
                {
                    deltaForce = _gravityApplier.Gravity.y * (_jumpGravityCoef + 1);
                }
                else
                {
                    deltaForce = _gravityApplier.Gravity.y * (_fallOfGravityCoef + 1);
                }

                deltaForce *= Time.deltaTime;
                
                _curJumpForce += deltaForce;
                
                _movementBehaviour.Move(jumpForce);

                timePassed += Time.deltaTime;
                
                yield return null;
            }
            
            OnJumpFinished?.Invoke();
        }
    }
}