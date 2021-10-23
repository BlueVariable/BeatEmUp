using UnityEngine;

namespace Common.DashScripts
{
    public abstract class DashCondition : MonoBehaviour
    {
        public abstract bool CanPerformDash();
    }
}