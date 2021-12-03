using BLUE.FSMSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class State_SimpleEnemy_Flee : StateBase<ESimpleEnemyState, FSMTransitionMessage_SimpleEnemy>
    {
        [SerializeField] private TriggerBase[] _fleeEnterRequestors = new TriggerBase[0];
        
        public override ESimpleEnemyState GetStateType()
        {
            return ESimpleEnemyState.Flee;
        }
    }
}