using System;
using BLUE.DamageSystem;
using Unity.Mathematics;
using UnityEngine;

namespace BLUE.ProjectX.Enemy.SimpleEnemy
{
    public class Shield_Enemy : MonoBehaviour
    {
        [SerializeField] private Collider _enemyDamagableCollider = null;

        [SerializeField] private Damagable _shieldDamagable = null;

        [SerializeField] private Transform _shieldVisual = null;

        [SerializeField] private Collider _movementBlocker = null;
        
        private void Awake()
        {
            EnableEnemyDamagableCollider(false);
            
            RegisterToShieldDamagable();
        }

        private void OnDestroy()
        {
            UnregisterFromShieldDamagable();
        }

        private void LateUpdate()
        {
            _shieldVisual.rotation = Quaternion.identity;
        }

        private void RegisterToShieldDamagable()
        {
            _shieldDamagable.OnDied += OnShieldDied;
        }

        private void UnregisterFromShieldDamagable()
        {
            _shieldDamagable.OnDied -= OnShieldDied;
        }

        private void OnShieldDied()
        {
            EnableEnemyDamagableCollider(true);

            _movementBlocker.enabled = false;
            
            _shieldVisual.gameObject.SetActive(false);
        }

        private void EnableEnemyDamagableCollider(bool val)
        {
            _enemyDamagableCollider.enabled = val;
        }
    }
}