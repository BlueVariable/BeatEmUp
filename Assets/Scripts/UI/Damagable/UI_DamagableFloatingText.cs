using System;
using BLUE.DamageSystem;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class UI_DamagableFloatingText : UIBase
    {
        #region Constants

        private const string FLOATING_TEXT_FORMAT = "0.";

        #endregion
        
        [SerializeField] private DamagableProviderBase _damagableProvider = null;

        [SerializeField] private UI_FloatingTextView _floatingTextView = null;

        [SerializeField] private Color _damageColor = Color.red;
        [SerializeField] private Color _healColor = Color.green;
        
        protected override void ActivatingCustomActions(Action onCompleted = null)
        {
            RegisterToDamagable();
            
            base.ActivatingCustomActions(onCompleted);
        }

        protected override void DeactivatingCustomActions(Action onCompleted = null)
        {
            UnregisterFromDamagable();
            
            base.DeactivatingCustomActions(onCompleted);
        }

        private void RegisterToDamagable()
        {
            _damagableProvider.Damagable.OnTookDamage += OnTookDamage;
            _damagableProvider.Damagable.OnHealed += OnHealed;
        }

        private void UnregisterFromDamagable()
        {
            _damagableProvider.Damagable.OnTookDamage -= OnTookDamage;
            _damagableProvider.Damagable.OnHealed -= OnHealed;
        }

        private void OnTookDamage(HealthUpdateInfo_Damage damage)
        {
            _floatingTextView.ActivateFloatingText(
                new FloatingTextData(
                    Mathf.Abs(damage.UpdateAmount).ToString(FLOATING_TEXT_FORMAT),
                    true,
                    _damageColor));
        }

        private void OnHealed(HealthUpdateInfo_Heal heal)
        {
            _floatingTextView.ActivateFloatingText(
                new FloatingTextData(
                    heal.UpdateAmount.ToString(FLOATING_TEXT_FORMAT),
                    true,
                    _healColor));
        }
    }
}