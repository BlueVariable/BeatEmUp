using System.Numerics;

namespace _Common.Movement
{
    public interface IMovementSpeedProvider
    {
        Vector3 GetMovementSpeed();
    }
}