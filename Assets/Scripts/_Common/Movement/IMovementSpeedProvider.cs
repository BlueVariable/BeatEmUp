using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public interface IMovementSpeedProvider
    {
        public Vector3 GetMovementSpeed();
    }
}