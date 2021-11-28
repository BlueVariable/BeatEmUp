using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ProjectX.Scene
{
    public class Action_TryLoadNextScene : ActionBase
    {
        [SerializeField] private bool _tryUnloadCurActiveScene = true;
        
        public override void Execute()
        {
            SceneManager.Instance.TryLoadNextScene(_tryUnloadCurActiveScene);
        }
    }
}