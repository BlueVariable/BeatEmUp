using BLUE.ActionSystem;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ProjectX.Weapon
{
    public class Action_PlayWeaponParticle : ActionBase
    {
        [SerializeField] private ParticlePlayer_Pool _particlePlayer = null;

        [SerializeField] private ParticlePlayData_Pool _playData = null;
        
        public override void Execute()
        {
            _playData.UpdateTargetParent(Character.Character.Instance.transform);
            
            _particlePlayer.Play(_playData);
        }
    }
}