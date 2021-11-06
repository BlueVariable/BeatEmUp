using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public class GravityApplier_CharacterController : MonoBehaviour
    {
        [SerializeField] private MovementBehaviour_CharacterController _movementBehaviour = null;

        [SerializeField] private Vector3 _gravity = new Vector3(0, -9.81f, 0);
        public Vector3 Gravity => _gravity;

        private bool _gravityBlocked = false;
        public bool IsGravityBlocked => _gravityBlocked;

        public void ToggleGravity(bool value)
        {
            _gravityBlocked = !value;
        }

        private void Update()
        {
            TryApplyGravity();
        }

        private void TryApplyGravity()
        {
            if (!_gravityBlocked)
            {
                _movementBehaviour.Move(_gravity);
            }
        }
    }
}