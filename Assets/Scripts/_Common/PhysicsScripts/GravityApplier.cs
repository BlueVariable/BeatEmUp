using UnityEngine;
using BLUE.ProjectX.Common.Movement;

namespace BLUE.ProjectX.Common.Physics
{
    public class GravityApplier : MonoBehaviour
    {
        [SerializeField] private MovementBehaviour_CharacterController _movementBehaviour = null;

        [SerializeField] private Vector3 _gravity = new Vector3(0, -9.81f, 0);

        public Vector3 Gravity => _gravity;

        private bool _canApply = true;

        public void ToggleGravity(bool value)
        {
            _canApply = value;
        }

        private void Update()
        {
            TryApplyGravity();
        }

        private void TryApplyGravity()
        {
            if (_canApply)
            {
                _movementBehaviour.Move(_gravity);
            }
        }
    }
}