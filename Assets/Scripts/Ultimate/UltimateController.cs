using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ProjectX.Ultimate
{
    public class UltimateController : MonoBehaviour
    {
        [SerializeField] private KeyCode _activatorKeyCode = KeyCode.Q;

        private UltimateBase[] _ultimates;
        private UltimateBase[] _Ultimates
        {
            get
            {
                if (_ultimates == null)
                {
                    _ultimates = GetComponentsInChildren<UltimateBase>();
                }

                return _ultimates;
            }
        }

        private UltimateBase _curUltimate;
        private UltimateBase _CurUltimate
        {
            get
            {
                if (_curUltimate == null)
                {
                    _curUltimate = GetComponentInChildren<UltimateBase>();
                }

                return _curUltimate;
            }
        }
        
        private void Awake()
        {
            RegisterToInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            }
        }

        private void OnKeyDown(InputData_Key inputData)
        {
            if (inputData.KeyCode != _activatorKeyCode)
            {
                return;
            }

            _CurUltimate.TryActivateUltimate();
        }
    }
}