using System.Linq;
using BLUE.SceneSystem;
using UnityEngine;

namespace BLUE.ProjectX.Scene
{
    public class SceneCondition_Type : SceneConditionBase<EScene>
    {
        [SerializeField] private EScene[] _lockedSceneTypes = default;
        
        public override bool CanProcessScene(EScene sceneType)
        {
            return !_lockedSceneTypes.Contains(sceneType);
        }
    }
}