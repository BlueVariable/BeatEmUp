using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public class GravityApplier_MovementBehaviourBased : GravityApplierBase
    {
        [SerializeField] private MovementBehaviourBase _movementBehaviour = null;

        protected override void ApplyGravity(Vector3 gravity)
        {
            _movementBehaviour.Move(gravity);
        }
    }
}