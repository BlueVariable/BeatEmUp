using UnityEngine;
using BLUE.ProjectX.Common.Movement;

namespace BLUE.ProjectX.Common.Physics
{
    public class GravityApplier : MonoBehaviour
    {
        [SerializeField] private MovementBehaviour_CharacterController _movementBehaviour = null;

        [SerializeField] private Vector3 _gravity = new Vector3(0, -9.81f, 0);

        public Vector3 Gravity => _gravity;

        private void Update()
        {
            _movementBehaviour.Move(_gravity);
        }
    }
}