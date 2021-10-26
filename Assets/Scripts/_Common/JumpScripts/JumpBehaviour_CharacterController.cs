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
            _curJumpForce = _jumpForce;
        }

        public void CancelJump()
        {
            StopJumpRoutine();
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

            _curJumpForce = _jumpForce;

            float timePassed = 0;

            while (!_characterController.isGrounded || timePassed < 0.25f)
            {
                Vector3 jumpForce = new Vector3(0, _curJumpForce, 0);
                
                float deltaVelocity = _curJumpForce > 0
                    ? _gravityApplier.Gravity.y * _jumpGravityCoef
                    : _gravityApplier.Gravity.y * _fallOfGravityCoef;

                deltaVelocity *= Time.deltaTime;

                _curJumpForce += deltaVelocity;
                
                _movementBehaviour.Move(jumpForce);

                timePassed += Time.deltaTime;
                
                yield return null;
            }
            
            OnLanded?.Invoke();
        }
    }
}