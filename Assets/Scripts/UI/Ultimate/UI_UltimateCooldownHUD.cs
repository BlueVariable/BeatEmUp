using BLUE.Helpers;
using BLUE.ProjectX.Ultimate;
using BLUE.UISystem;
using UnityEngine;

namespace UI.Ultimate
{
    [RequireComponent(typeof(IUltimateControllerProvider))]
    public class UI_UltimateCooldownHUD : UIBase
    {
        [SerializeField] private UI_FillBar _fillBar = null;
        
        private IUltimateControllerProvider _ultimateControllerProvider;

        private IUltimateControllerProvider _UltimateControllerProvider
        {
            get
            {
                if (_ultimateControllerProvider == null)
                {
                    _ultimateControllerProvider = GetComponent<IUltimateControllerProvider>();
                }

                return _ultimateControllerProvider;
            }
        }

        protected override void ActivatedCustomActions()
        {
            CoroutineHelper.Instance.WaitForSeconds(0.05f, RegisterToUltimate);
            
            base.ActivatedCustomActions();
        }

        protected override void DeactivatedCustomActions()
        {
            UnregisterFromUltimate();
            
            base.DeactivatedCustomActions();
        }

        private void RegisterToUltimate()
        {
            if (!(_UltimateControllerProvider.GetUltimateController().CurUltimate is ICooldownUltimate))
            {
                TryDeactivate();
            }

            ICooldownUltimate cooldownUltimate = _UltimateControllerProvider.GetUltimateController().CurUltimate as ICooldownUltimate;

            cooldownUltimate.CooldownTimer.OnTimerUpdated += OnTimerUpdated;

        }

        private void UnregisterFromUltimate()
        {
            ICooldownUltimate cooldownUltimate = _UltimateControllerProvider.GetUltimateController().CurUltimate as ICooldownUltimate;

            cooldownUltimate.CooldownTimer.OnTimerUpdated -= OnTimerUpdated;
        }

        private void OnTimerUpdated(float normalizedProgress)
        {
            _fillBar.SetTargetNormValue(normalizedProgress, true);    
        }
    }
}