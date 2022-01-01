using BLUE.DamageSystem;
using UnityEngine;

namespace BLUE.ProjectX.Shield
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private Collider _selfDamagableCollider = null;

        [SerializeField] private Damagable _shieldDamagable = null;

        [SerializeField] private Transform _shieldVisual = null;

        [SerializeField] private Collider _movementBlocker = null;
        
        private void Awake()
        {
            SetDamagableColliderEnabled(false);
            
            RegisterToShieldDamagable();
        }
        
        private void LateUpdate()
        {
            _shieldVisual.rotation = Quaternion.identity;
        }

        private void OnDestroy()
        {
            UnregisterFromShieldDamagable();
        }

        private void RegisterToShieldDamagable()
        {
            _shieldDamagable.OnDied += OnShieldDestroyed;
        }

        private void UnregisterFromShieldDamagable()
        {
            _shieldDamagable.OnDied -= OnShieldDestroyed;
        }

        private void OnShieldDestroyed()
        {
            SetDamagableColliderEnabled(true);

            SetMovementBlockerEnabled(false);

            SetShieldVisualActive(false);
        }

        private void SetDamagableColliderEnabled(bool val)
        {
            _selfDamagableCollider.enabled = val;
        }
        
        private void SetMovementBlockerEnabled(bool val)
        {
            _movementBlocker.enabled = val;
        }

        private void SetShieldVisualActive(bool val)
        {
            _shieldVisual.gameObject.SetActive(val);
        }
    }
}