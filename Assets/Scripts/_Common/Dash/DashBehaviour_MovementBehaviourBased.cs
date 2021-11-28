using System;
using UnityEngine;
using System.Collections;

namespace BLUE.ProjectX.Common
{
    public class DashBehaviour_MovementBehaviourBased : DashBehaviourBase
    {
        [SerializeField] private MovementBehaviourBase _movementBehaviour = null;
        
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

        #region Events

        public override Action OnDashStarted { get; set; }
        
        public override Action OnDashFinished { get; set; }

        #endregion
        
        public override void Dash(Vector3 dashDirection)
        {
            StartDashRoutine(dashDirection);
        }
        
        private void StartDashRoutine(Vector3 dashDirection)
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

        private IEnumerator DashProgress(Vector3 dashDirection)
        {
            OnDashStarted?.Invoke();
            
            float timePassed = 0;

            while (timePassed < _dashDuration)
            {
                timePassed += Time.deltaTime;

                Vector3 movementSpeed = dashDirection * _SpeedProvider.GetDashSpeed(timePassed / _dashDuration);
                
                _movementBehaviour.Move(movementSpeed);
                
                yield return null;
            }
            
            OnDashFinished?.Invoke();
        }
    }
}