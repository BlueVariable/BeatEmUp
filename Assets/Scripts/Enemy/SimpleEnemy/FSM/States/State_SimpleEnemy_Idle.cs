using BLUE.FSMSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class State_SimpleEnemy_Idle : StateBase<ESimpleEnemyState, FSMTransitionMessage_SimpleEnemy>
    {
        [SerializeField] private TriggerBase[] _idleEnterRequestors = new TriggerBase[0];

        public override ESimpleEnemyState GetStateType()
        {
            return ESimpleEnemyState.Idle;
        }
    }
}