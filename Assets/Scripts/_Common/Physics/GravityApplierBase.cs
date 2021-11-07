using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public abstract class GravityApplierBase : MonoBehaviour
    {
        [SerializeField] private Vector3 _gravity = new Vector3(0, -9.81f, 0);
        public Vector3 Gravity => _gravity;
        
        private bool _gravityBlocked = false;
        public bool IsGravityBlocked => _gravityBlocked;
        
        protected abstract void ApplyGravity(Vector3 gravity);
        
        public void ToggleGravity(bool value)
        {
            _gravityBlocked = !value;
        }
        
        private void Update()
        {
            if (_gravityBlocked)
            {
                return;
            }
            
            ApplyGravity(Gravity);
        }
    }
}