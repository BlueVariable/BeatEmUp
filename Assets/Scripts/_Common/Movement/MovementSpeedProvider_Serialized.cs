using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public class MovementSpeedProvider_Serialized : MonoBehaviour,
        IMovementSpeedProvider
    {
        [SerializeField] private Vector3 _movementSpeed = new Vector3(2.5f, 0, 0);
        
        public Vector3 GetMovementSpeed()
        {
            return _movementSpeed;
        }
    }
}