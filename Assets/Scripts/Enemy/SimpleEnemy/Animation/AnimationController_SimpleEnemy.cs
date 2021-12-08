using System;
using System.Collections.Generic;
using System.Linq;
using BLUE.AnimationSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    // TODO GOKAY - Move Full Path State Code to Base Class
    public class AnimationController_SimpleEnemy : AnimationController<ESimpleEnemyAnimation>
    {
        #region Constants

        public const int BASE_LAYER = 1;

        public const string BASE_LAYER_NAME = "Base Layer";

        #endregion

        public Animator Animator => _animator;

        private Dictionary<int, string> _layerIDNameMap = new Dictionary<int, string>()
        {
            { BASE_LAYER, BASE_LAYER_NAME },
        };

        public string GetFullPathOfState(ESimpleEnemyAnimation animType, int layerIndex)
        {
            if (!_layerIDNameMap.ContainsKey(layerIndex))
            {
                throw new Exception($"@GetFullPathOfState layer index could not be found!");
            }

            AnimationInfo info = _animationInfoColl.SingleOrDefault(i => i.AnimationType == animType);

            if (info == null)
            {
                throw new Exception($"@GetFullPathOfState animation type: {animType} could not be found!" +
                                    $" Maintain animation info collection serialization.");
            }
            
            string layerName = _layerIDNameMap[layerIndex];

            return layerName + "." + info.AnimationStateName;
        }
    }
}