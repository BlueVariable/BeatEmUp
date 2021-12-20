using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class UI_FloatingTextCarrier : MonoBehaviour
    {
        private static UI_FloatingTextCarrier _instance;

        public static UI_FloatingTextCarrier Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UI_FloatingTextCarrier>();
                }

                return _instance;
            }
        }
    }
}