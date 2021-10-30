using UnityEngine;

namespace BLUE.ProjectX.Common.Dash
{
    public abstract class DashCondition : MonoBehaviour
    {
        public abstract bool CanPerformDash();
    }
}