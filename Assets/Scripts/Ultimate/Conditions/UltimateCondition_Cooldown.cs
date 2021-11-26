using UnityEngine;

namespace BLUE.ProjectX.Ultimate
{
    public class UltimateCondition_Cooldown : UltimateConditionBase
    {
        [SerializeField] private UltimateTimerProvider _ultimateTimerProvider = null;
        
        public override bool IsConditionSatisfied()
        {
            if (_ultimateTimerProvider.GetUltimateTimer().IsTimerRunning)
            {
                return false;
            }

            return true;
        }
    }
}