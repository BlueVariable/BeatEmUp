using BLUE.Common;
using BLUE.ActionSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public class Action_PlayParticleOnTriggerEnterCollider : ActionBase
    {
        [SerializeField] private Transform _targetTransform = null;

        [SerializeField] private Trigger_OnTriggerEnter _trigger = null;

        [SerializeField] private ParticlePlayer_Pool _particlePlayer = null;

        [SerializeField] private bool _alterRotationBasedOnNormal = true;
        
        public override void Execute()
        {
            Vector3 targetPos = _targetTransform.position;
            
            Vector3 triggerPos = _trigger.LastValidCollider.ClosestPoint(targetPos);

            if (_alterRotationBasedOnNormal)
            {
                Vector3 normal = (targetPos - triggerPos).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(normal, Vector3.up);
                
                _particlePlayer.Play(
                    new ParticlePlayData_Pool(
                        null,
                        targetPos, 
                        targetRotation.eulerAngles));
            }
            else
            {
                _particlePlayer.Play(
                    new ParticlePlayData_Pool(
                        null,
                        targetPos, Quaternion.identity.eulerAngles));
            }
        }
    }
}