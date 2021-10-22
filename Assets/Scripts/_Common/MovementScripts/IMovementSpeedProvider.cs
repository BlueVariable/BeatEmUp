using UnityEngine;

namespace Common.Movement
{
    public interface IMovementSpeedProvider
    {
        Vector3 GetMovementSpeed();
    }
}