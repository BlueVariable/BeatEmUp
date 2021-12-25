using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class Character : MonoBehaviour
    {
        #region Singleton

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

        #endregion
    }
}