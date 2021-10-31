using System;
using UnityEngine;
using System.Collections;
using BLUE.ProjectX.Common.Physics;
using BLUE.ProjectX.Common.Movement;

namespace BLUE.ProjectX.Common.JumpScripts
{
    public class JumpBehaviour_CharacterController : MonoBehaviour
    {
        [SerializeField] private MovementBehaviour_CharacterController _movementBehaviour = null;
        
        [SerializeField] private CharacterController _characterController = null;

        [SerializeField] private GravityApplier _gravityApplier = null;
        
        [SerializeField] private float _jumpForce = 10f;

        [SerializeField] private float _jumpGravityCoef = 1f;
        
        [SerializeField] private float _fallOfGravityCoef = 1f;

        private float _curJumpForce = 0;

        private IEnumerator _jumpRoutine;
        
        public Action OnJumpStarted { get; set; }
        
        public Action OnLanded { get; set; }
        
        public void Jump()
        {
            StartJumpRoutine();
        }

        public void DoubleJumpBoost()
        {
            _curJumpForce = _jumpForce + Mathf.Abs(_gravityApplier.Gravity.y);
        }

        public void CancelJump()
        {
            StopJumpRoutine();
        }

        public void PassIntoFalling()
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
            
            OnLanded?.Invoke();
        }

        private float GetJumpDuration()
        {
            float tup = _jumpForce / Math.Abs(_gravityApplier.Gravity.y * (_jumpGravityCoef + 1));

            float hmax = (_jumpForce * _jumpForce) / (2 * Math.Abs(_gravityApplier.Gravity.y * (_jumpGravityCoef + 1)));

            float tdown = (float) Math.Sqrt((2 * hmax) / Math.Abs(_gravityApplier.Gravity.y * (_fallOfGravityCoef + 1)));

            return tup + tdown;
        }
    }
}