using UnityEngine;

namespace BLUE.ProjectX.Common.Movement
{
    public interface IMovementSpeedProvider
    {
        Vector3 GetMovementSpeed();
    }
}