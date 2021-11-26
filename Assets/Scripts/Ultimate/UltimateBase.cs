using System;
using UnityEngine;

namespace BLUE.ProjectX.Ultimate
{
    public enum EUltimate
    {
        None = 0,
        Heal = 10
    }
    
    public abstract class UltimateBase : MonoBehaviour
    {
        public abstract EUltimate UltimateType { get; }
        
        private UltimateConditionBase[] _conditions;

        private UltimateConditionBase[] _Conditions
        {
            get
            {
                if (_conditions == null)
                {
                    _conditions = GetComponentsInChildren<UltimateConditionBase>();
                }

                return _conditions;
            }
        }
        
        public Action OnUltimateStarted { get; set; }
        public Action OnUltimateCompleted { get; set; }

        public bool TryActivateUltimate()
        {
            foreach (UltimateConditionBase condition in _Conditions)
            {
                if (!condition.IsConditionSatisfied())
                {
                    return false;
                }
            }
            
            OnUltimateStarted?.Invoke();

            ActivatedCustomActions();
            
            return true;
        }

        public void InterruptUltimate()
        {
            CompleteUltimate();            
        }

        protected void CompleteUltimate()
        {
            DeactivatedCustomActions();
            
            OnUltimateCompleted?.Invoke();
        }

        protected virtual void ActivatedCustomActions() { }
        
        protected virtual void DeactivatedCustomActions() { }
    }
}