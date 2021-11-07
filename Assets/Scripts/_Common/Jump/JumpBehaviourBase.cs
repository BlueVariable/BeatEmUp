using System;
using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public abstract class JumpBehaviourBase : MonoBehaviour
    {
        #region Events

        public abstract Action OnJumpStarted { get; set; }
        
        public abstract Action OnJumpFinished { get; set; }

        #endregion
        
        public abstract void StartJump();

        public abstract void AddDoubleJumpBoost();
        
        public abstract void StopJump();

        public abstract void ResetJumpForceToGravity();
    }
}