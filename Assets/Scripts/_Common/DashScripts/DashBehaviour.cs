using System;
using UnityEngine;
using System.Collections;
using BLUE.ProjectX.Common.Movement;

namespace BLUE.ProjectX.Common.DashScripts
{
    [RequireComponent(typeof(IDashSpeedProvider))]
    public class DashBehaviour : MonoBehaviour
    {
        [SerializeField] private MovementBehaviour_CharacterController _movementBehaviour = null;
        
        [SerializeField] private float _dashDuration = 1f;
        
        private IEnumerator _dashRoutine;
        
        private IDashSpeedProvider _speedProvider;

        private IDashSpeedProvider _SpeedProvider
        {
            get
            {
                if (_speedProvider == null)
                {
                    _speedProvider = GetComponent<IDashSpeedProvider>();
                }

                return _speedProvider;
            }
        }
        
        public Action OnDashStarted { get; set; }
        
        public Action OnDashCompleted { get; set; }
        
        public void Dash(int dashDirection)
        {
            StartDashRoutine(dashDirection);
        }
        
        private void StartDashRoutine(int dashDirection)
        {
            StopDashRoutine();

            _dashRoutine = DashProgress(dashDirection);

            StartCoroutine(_dashRoutine);
        }

        private void StopDashRoutine()
        {
            if (_dashRoutine != null)
            {
                StopCoroutine(_dashRoutine);
            }
        }

        private IEnumerator DashProgress(int dashDirection)
        {
            OnDashStarted?.Invoke();
            
            float timePassed = 0;

            while (timePassed < _dashDuration)
            {
                timePassed += Time.deltaTime;

                float movementSpeed = dashDirection * _SpeedProvider.GetDashSpeed(timePassed / _dashDuration);
                
                _movementBehaviour.Move(new Vector3(movementSpeed, 0, 0));
                
                yield return null;
            }
            
            OnDashCompleted?.Invoke();
        }
    }
}