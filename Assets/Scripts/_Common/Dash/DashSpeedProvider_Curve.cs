using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public class DashSpeedProvider_Curve : MonoBehaviour, IDashSpeedProvider
    {
        [SerializeField] private float _dashSpeedCoef = 15f;

        [SerializeField] private AnimationCurve _curve = default;
        
        public float GetDashSpeed(float normalizedDashTime)
        {
            return _dashSpeedCoef * _curve.Evaluate(normalizedDashTime);
        }
    }
}