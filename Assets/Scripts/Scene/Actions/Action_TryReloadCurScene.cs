using BLUE.ActionSystem;

namespace BLUE.ProjectX.Scene
{
    public class Action_TryReloadCurScene : ActionBase
    {
        public override void Execute()
        {
            SceneManager.Instance.TryReloadCurScene();
        }
    }
}
