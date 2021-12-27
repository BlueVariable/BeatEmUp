using BLUE.ActionSystem;
using BLUE.DamageSystem;
using BLUE.UISystem;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace BLUE.ProjectX.UI
{
    public class UI_DamagableHealthHUD : UIBase
    {
        [SerializeField] private UI_FillBar _fillBar = null;

        [SerializeField] private DamagableProviderBase _damagableProvider = null;

        [SerializeField] private ActionExecutor _onHealedActions = null;
        [SerializeField] private ActionExecutor _onTookDamageActions = null;
        [SerializeField] private ActionExecutor _onDiedActions = null;

        [SerializeField] private UI_Text _curHealthText = null;
        [SerializeField] private UI_Text _maxHealthText = null;
        
        protected override void ActivatedCustomActions()
        {
            InitBar();

            RegisterToDamagable();
            
            base.ActivatedCustomActions();
        }

        protected override void DeactivatedCustomActions()
        {
            UnregisterFromDamagable();

            base.DeactivatedCustomActions();
        }

        private void RegisterToDamagable()
        {
            _damagableProvider.Damagable.OnHealed += OnHealed;
            _damagableProvider.Damagable.OnTookDamage += OnTookDamage;
            _damagableProvider.Damagable.OnDied += OnDied;
        }

        private void UnregisterFromDamagable()
        {
            _damagableProvider.Damagable.OnHealed -= OnHealed;
            _damagableProvider.Damagable.OnTookDamage -= OnTookDamage;
            _damagableProvider.Damagable.OnDied -= OnDied;
        }

        private void OnHealed(HealthUpdateInfo_Heal info)
        {
            UpdateBar();
            
            _onHealedActions.Execute();
        }

        private void OnTookDamage(HealthUpdateInfo_Damage info)
        {
            UpdateBar();
            
            _onTookDamageActions.Execute();
        }

        private void OnDied()
        {
            UpdateBar();
            
            _onDiedActions.Execute();
        }

        private void InitBar()
        {
            UpdateBar();
            
            _fillBar.SetTargetNormValue(1, true);
        }

        private void UpdateBar()
        {
            if (_curHealthText)
            {
                _curHealthText.Text = _damagableProvider.Damagable.CurHP + "";
            }

            if (_maxHealthText)
            {
                _maxHealthText.Text = _damagableProvider.Damagable.MaxHP + "";
            }
            
            float normValue = _damagableProvider.Damagable.CurHP / _damagableProvider.Damagable.MaxHP;
            
            _fillBar.SetTargetNormValue(normValue);
        }
    }
}