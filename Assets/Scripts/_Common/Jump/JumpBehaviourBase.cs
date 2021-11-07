using System;
using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public abstract class JumpBehaviourBase : MonoBehaviour
    {
        public abstract Action OnJumpStarted { get; set; }
        
        public abstract Action OnJumpFinished { get; set; }
        
        public abstract void StartJump();

        public abstract void DoubleJumpBoost();
        
        public abstract void StopJump();

        public abstract void ResetJumpForceToGravity();
    }
}