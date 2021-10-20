using System.Linq;
using BLUE.SceneSystem;
using UnityEngine;

namespace BLUE.ProjectX.Scene
{
    public class SceneCondition_GameScene_Type : SceneConditionBase<EGameScene>
    {
        [SerializeField] private EGameScene[] _lockedSceneTypes = default;
        
        public override bool CanProcessScene(EGameScene gameSceneType)
        {
            return !_lockedSceneTypes.Contains(gameSceneType);
        }
    }
}