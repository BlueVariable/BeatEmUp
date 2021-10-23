using BLUE.Helpers;
using UnityEngine;

namespace BLUE.ProjectX.Common.DashScripts
{
    public class DashCondition_Cooldown : DashCondition
    {
        [SerializeField] private DashBehaviour _dashBehaviour = null;

        [SerializeField] private float _cooldown = 5f;
        
        private bool _inCooldown = false;
        
        public override bool CanPerformDash()
        {
            return !_inCooldown;
        }

        private void Awake()
        {
            RegisterToDashBehaviour();
        }

        private void OnDestroy()
        {
            UnregisterFromDashBehaviour();
        }

        private void RegisterToDashBehaviour()
        {
            _dashBehaviour.OnDashStarted += OnDashStarted;
        }

        private void UnregisterFromDashBehaviour()
        {
            _dashBehaviour.OnDashStarted -= OnDashStarted;
        }

        private void OnDashStarted()
        {
            _inCooldown = true;

            CoroutineHelper.Instance.WaitForSeconds(_cooldown, () =>
            {
                _inCooldown = false;
            });
        }
    }
}