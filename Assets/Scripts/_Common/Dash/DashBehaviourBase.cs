using System;
using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public abstract class DashBehaviourBase : MonoBehaviour
    {
        #region Events

        public abstract Action OnDashStarted { get; set; }
        
        public abstract Action OnDashFinished { get; set; }

        #endregion
        
        public abstract void Dash(Vector3 dashDirection);
    }
}