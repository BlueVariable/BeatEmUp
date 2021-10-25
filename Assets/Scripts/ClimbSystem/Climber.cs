using UnityEngine;

namespace BLUE.ProjectX.ClimbSystem
{
    public class Climber : MonoBehaviour
    {
        [SerializeField] private Transform _climbControl = null;

        [SerializeField] private float _climbableControlRadius = 1;

        [SerializeField] private float _climableControlHeight = 1;
        
        public bool CheckCanHoldClimbable()
        {
            Collider[] colliders = Physics.OverlapSphere(_climbControl.position, _climbableControlRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Climbable climbable))
                {
                    return true;
                }
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            
            Gizmos.DrawWireSphere(_climbControl.position, _climbableControlRadius);
            
            Gizmos.DrawLine(
                _climbControl.position + new Vector3(0, _climableControlHeight / 2, 0), 
                _climbControl.position - new Vector3(0, _climableControlHeight / 2, 0));
        }
    }
}