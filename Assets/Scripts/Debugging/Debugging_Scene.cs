using BLUE.ProjectX.Scene;
using UnityEngine;

namespace BLUE.ProjectX.Debugging
{
    public class Debugging_Scene : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.Instance.TryLoadNextScene(true);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.Instance.TryReloadCurScene();
            }
        }
    }
}