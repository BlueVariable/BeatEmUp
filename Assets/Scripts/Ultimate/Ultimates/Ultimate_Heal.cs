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
        
        public Timer CooldownTimer => _cooldownTimer;
        
        public override EUltimate UltimateType => EUltimate.Heal;

        private Damagable _damagable;

        private Damagable _Damagable
        {
            get
            {
                if (_damagable == null)
                {
                    _damagable = GetComponentInParent<Damagable>();
                }

                return _damagable;
            }
        }

        protected override void ActivatedCustomActions()
        {
            _cooldownTimer.StartTimer();
            
            _Damagable.RefillHP(_healSource);
            
            CompleteUltimate();
            
            base.ActivatedCustomActions();
        }
    }
}