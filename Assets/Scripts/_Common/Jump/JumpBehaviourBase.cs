using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public abstract class JumpBehaviourBase : MonoBehaviour
    {
        public abstract void StartJump();

        public abstract void DoubleJumpBoost();
        
        public abstract void StopJump();

        public abstract void ResetJumpForceToGravity();
    }
}