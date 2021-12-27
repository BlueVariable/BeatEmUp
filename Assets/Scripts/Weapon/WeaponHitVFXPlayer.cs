using BLUE.Common;
using UnityEngine;

namespace BLUE.ProjectX.Weapon
{
    public class WeaponHitVFXPlayer : MonoBehaviour
    {
        [SerializeField] private AttackBehaviourBase _attackBehaviour = null;
        
        [SerializeField] private ParticlePlayer_Pool _particlePlayer = null;

        [SerializeField] private ParticlePlayData_Pool _particleData = null;
        
        private void Awake()
        {
            RegisterToAttackBehaviour();
        }

        private void OnDestroy()
        {
            UnregisterFromAttackBehaviour();
        }

        private void RegisterToAttackBehaviour()
        {
            _attackBehaviour.OnAttacked += OnAttacked;
        }

        private void UnregisterFromAttackBehaviour()
        {
            _attackBehaviour.OnAttacked -= OnAttacked;
        }

        private void OnAttacked(Transform target)
        {
            ParticlePlayData_Pool playData =
                new ParticlePlayData_Pool(
                    target, 
                    _particleData.PositionOffset,
                    _particleData.RotationOffset);
            
            _particlePlayer.Play(playData);
        }
    }
}