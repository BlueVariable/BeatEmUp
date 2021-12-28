using System;
using BLUE.DamageSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy.SimpleEnemy
{
    public class Shield_Enemy : MonoBehaviour
    {
        [SerializeField] private Collider _enemyDamagableCollider = null;

        [SerializeField] private Damagable _shieldDamagable = null;

        private void Awake()
        {
            EnableEnemyDamagableCollider(false);
            
            RegisterToShieldDamagable();
        }

        private void OnDestroy()
        {
            UnregisterFromShieldDamagable();
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
        }

        private void EnableEnemyDamagableCollider(bool val)
        {
            _enemyDamagableCollider.enabled = val;
        }
    }
}