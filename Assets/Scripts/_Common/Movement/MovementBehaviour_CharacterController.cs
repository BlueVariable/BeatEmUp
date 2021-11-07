using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public class MovementBehaviour_CharacterController : MovementBehaviourBase
    {
        [SerializeField] private CharacterController _characterController = null;

        private Vector3 _motion = Vector3.zero;

        public override void Move(Vector3 velocity)
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