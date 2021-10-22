using UnityEngine;

namespace _Common.Movement
{
    public interface IMovementSpeedProvider
    {
        Vector3 GetMovementSpeed();
    }
}