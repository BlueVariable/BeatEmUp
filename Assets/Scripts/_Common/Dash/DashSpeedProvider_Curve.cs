using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public class DashSpeedProvider_Curve : MonoBehaviour, IDashSpeedProvider
    {
        [SerializeField] private float _dashSpeed = 5f;

        [SerializeField] private AnimationCurve _curve = default;
        
        public float GetDashSpeed(float normalizedDashTime)
        {
            return _dashSpeed * _curve.Evaluate(normalizedDashTime);
        }
    }
}