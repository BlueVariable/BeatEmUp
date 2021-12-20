using System;
using BLUE.DamageSystem;
using BLUE.Helpers;
using UnityEngine;

namespace BLUE.ProjectX.Ultimate
{
    [RequireComponent(typeof(HealSource))] 
    public class Ultimate_Heal : UltimateBase, ICooldownUltimate
    {
        [SerializeField] private Timer _cooldownTimer = new Timer();
        
        [SerializeField] private HealSource _healSource = null;
        
        public override EUltimate UltimateType => EUltimate.Heal;

        public Timer CooldownTimer => _cooldownTimer;

        private DamagableProviderBase _damagableProvider;

        private DamagableProviderBase _DamagableProvider
        {
            get
            {
                if (_damagableProvider == null)
                {
                    _damagableProvider = GetComponentInParent<DamagableProviderBase>();
                }

                return _damagableProvider;
            }
        }

        private void Awake()
        {
            _cooldownTimer.StartTimer();
        }

        protected override void ActivatedCustomActions()
        {
            _cooldownTimer.StartTimer();
            
            _DamagableProvider.Damagable.RefillHP(_healSource);
            
            DeactivateUltimate();
            
            base.ActivatedCustomActions();
        }
    }
}