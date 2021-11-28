using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public abstract class RotationBehaviourBase : MonoBehaviour
    {
        public abstract void Rotate(Vector3 targetRotation);
    }
}