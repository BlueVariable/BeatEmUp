using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class Character : MonoBehaviour
    {
        private static Character _instance;

        public static Character Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Character>();
                }

                return _instance;
            }
        }
    }
}