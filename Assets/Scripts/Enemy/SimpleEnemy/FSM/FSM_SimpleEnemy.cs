using System.Collections.Generic;
using BLUE.FSMSystem;

namespace BLUE.ProjectX.Enemy
{
    public class FSM_SimpleEnemy : FSMBase<ESimpleEnemyState, FSMTransitionMessage_SimpleEnemy, FSMTransition_SimpleEnemy>
    {
        protected override List<FSMTransition_SimpleEnemy> GetInitialPossibleTransitions()
        {
            return new List<FSMTransition_SimpleEnemy>
            {
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.None, ESimpleEnemyState.Idle),
                
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Idle, ESimpleEnemyState.Wander),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Idle, ESimpleEnemyState.Chase),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Idle, ESimpleEnemyState.Attack),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Idle, ESimpleEnemyState.Flee),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Idle, ESimpleEnemyState.Death),
                
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Wander, ESimpleEnemyState.Idle),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Wander, ESimpleEnemyState.Chase),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Wander, ESimpleEnemyState.Flee),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Wander, ESimpleEnemyState.Death),
                    
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Chase, ESimpleEnemyState.Idle),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Chase, ESimpleEnemyState.Attack),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Chase, ESimpleEnemyState.Flee),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Chase, ESimpleEnemyState.Death),

                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Attack, ESimpleEnemyState.Idle),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Attack, ESimpleEnemyState.Flee),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Attack, ESimpleEnemyState.Death),
                
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Flee, ESimpleEnemyState.Idle),
                new FSMTransition_SimpleEnemy(ESimpleEnemyState.Flee, ESimpleEnemyState.Death),
            };
        }
    }
}