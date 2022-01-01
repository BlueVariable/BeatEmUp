using BLUE.Common;
using BLUE.ActionSystem;
using BLUE.TriggerSystem;
using BLUE.ProjectileSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class Action_PlayProjectileParticleOnTrigger : ActionBase
    {
        [SerializeField] private Projectile _projectile = null;

        [SerializeField] private Trigger_OnTriggerEnter _trigger = null;

        [SerializeField] private ParticlePlayer_Pool _particlePlayer = null;
        
        public override void Execute()
        {
            Vector3 projectilePos = _projectile.transform.position;
            
            Vector3 triggerPos = _trigger.LastValidCollider.ClosestPoint(projectilePos);

            Vector3 impactNormal = (projectilePos - triggerPos).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(impactNormal, Vector3.up);
            
            _particlePlayer.Play(
                new ParticlePlayData_Pool(
                    null,
                    projectilePos, 
                    targetRotation.eulerAngles));
        }
    }
}