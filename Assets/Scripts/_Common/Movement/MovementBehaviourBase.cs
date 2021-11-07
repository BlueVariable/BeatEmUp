using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public abstract class MovementBehaviourBase : MonoBehaviour
    {
        public abstract void Move(Vector3 velocity);
    }
}