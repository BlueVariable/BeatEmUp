using BLUE.Helpers;
using UnityEngine;

namespace BLUE.ProjectX.Ultimate
{
    [RequireComponent(typeof(ICooldownUltimate))]
    public class UltimateCooldownTimerProvider : MonoBehaviour
    {
        private ICooldownUltimate _cooldownUltimate;

        private ICooldownUltimate _CooldownUltimate
        {
            get
            {
                if (_cooldownUltimate == null)
                {
                    _cooldownUltimate = GetComponent<ICooldownUltimate>();
                }

                return _cooldownUltimate;
            }
        }

        public Timer GetUltimateTimer()
        {
            return _CooldownUltimate.CooldownTimer;
        }
    }
}