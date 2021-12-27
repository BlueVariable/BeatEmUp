using BLUE.Common;
using UnityEngine;

namespace BLUE.ProjectX.Attack
{
    public class AttackParticlePlayer : MonoBehaviour
    {
        [SerializeField] private AttackBehaviourBase _attackBehaviour = null;
        
        [SerializeField] private ParticlePlayer_Pool _particlePlayer = null;

        [SerializeField] private Vector3 _rotationOffset = Vector3.zero;

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
            if (target == null)
            {
                return;
            }
            
            ParticlePlayData_Pool playData =
                new ParticlePlayData_Pool(
                    null, 
                    target.position,
                    _rotationOffset);
            
            _particlePlayer.Play(playData);
        }
    }
}