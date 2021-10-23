using UnityEngine;

namespace BLUE.ProjectX.Common.DashScripts
{
    public abstract class DashCondition : MonoBehaviour
    {
        public abstract bool CanPerformDash();
    }
}