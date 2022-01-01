using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ProjectX.Scene
{
    public class Action_TryLoadScene : ActionBase
    {
        [SerializeField] private EScene _sceneType = EScene.Bluentangled;
        
        [SerializeField] private bool _unloadCurActiveScene = true;
        
        public override void Execute()
        {
            SceneManager.Instance.TryLoadSceneByType(_sceneType, _unloadCurActiveScene);
        }
    }
}