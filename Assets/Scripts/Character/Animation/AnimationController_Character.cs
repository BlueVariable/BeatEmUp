using System;
using System.Collections.Generic;
using System.Linq;
using BLUE.AnimationSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class AnimationController_Character : AnimationController<ECharacterAnimation>
    {
        #region Constants

        public const int MOVEMENT_LAYER = 1;
        public const int ATTACK_LAYER = 2;

        public const string MOVEMENT_LAYER_NAME = "Movement Layer";
        public const string ATTACK_LAYER_NAME = "Attack Layer";

        #endregion

        public Animator Animator => _animator;

        private Dictionary<int, string> _layerIdNameMap = new Dictionary<int, string>()
        {
            { MOVEMENT_LAYER, MOVEMENT_LAYER_NAME },
            { ATTACK_LAYER, ATTACK_LAYER_NAME },
        };
        
        public void UpdateParameter(string paramName, float value)
        {
            _animator.SetFloat(paramName, value);   
        }

        public void SetLayerWeight(int layerIndex, float val)
        {
            _animator.SetLayerWeight(layerIndex, val);
        }

        public string GetFullPathOfState(ECharacterAnimation animType, int layerIndex)
        {
            if (!_layerIdNameMap.ContainsKey(layerIndex))
            {
                throw new Exception($"@GetFullPathOfState layer index could not found!..");
            }

            AnimationInfo info = _animationInfoColl.SingleOrDefault(i => i.AnimationType == animType);

            if (info == null)
            {
                throw new Exception($"@GetFullPathOfState animation type : {animType} could not found!.. Check Animation Info collection serilization.");
            }
            
            string layerName = _layerIdNameMap[layerIndex];

            return layerName + "." + info.AnimationStateName;
        }
    }
}