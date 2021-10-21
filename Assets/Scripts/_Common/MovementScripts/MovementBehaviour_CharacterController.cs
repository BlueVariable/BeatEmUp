using UnityEngine;

namespace _Common.Movement
{
    public class MovementBehaviour_CharacterController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController = null;

        private Vector3 _motion = Vector3.zero;

        public void Move(Vector3 velocity)
        {
            _motion += velocity;
        }
        
        private void Update()
        {
            _characterController.Move(_motion * Time.deltaTime);

            _motion = Vector3.zero;
        }
    }
}