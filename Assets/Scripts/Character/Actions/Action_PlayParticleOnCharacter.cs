using BLUE.ActionSystem;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class Action_PlayParticleOnCharacter : ActionBase
    {
        [SerializeField] private ParticlePlayer_Pool _particlePlayer = null;

        [SerializeField] private ParticlePlayData_Pool _playData = null;
        
        public override void Execute()
        {
            _playData = new ParticlePlayData_Pool(Character.Instance.transform, _playData.PositionOffset,
                _playData.RotationOffset);

            _particlePlayer.Play(_playData);
        }
    }
}