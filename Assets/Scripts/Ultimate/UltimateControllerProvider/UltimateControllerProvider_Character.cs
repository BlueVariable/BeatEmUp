using UnityEngine;

namespace BLUE.ProjectX.Ultimate
{
    public class UltimateControllerProvider_Character : MonoBehaviour, IUltimateControllerProvider
    {
        private UltimateController _ultimateController;

        private UltimateController _UltimateController
        {
            get
            {
                if (_ultimateController == null)
                {
                    _ultimateController = Character.Character.Instance.GetComponentInChildren<UltimateController>();
                }

                return _ultimateController;
            }
        }
        
        public UltimateController GetUltimateController()
        {
            return _UltimateController;
        }
    }
}