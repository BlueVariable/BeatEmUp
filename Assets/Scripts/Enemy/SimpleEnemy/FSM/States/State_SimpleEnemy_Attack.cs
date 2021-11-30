using BLUE.FSMSystem;

namespace BLUE.ProjectX.Enemy
{
    public class State_SimpleEnemy_Attack : StateBase<ESimpleEnemyState, FSMTransitionMessage_SimpleEnemy>
    {
        public override ESimpleEnemyState GetStateType()
        {
            return ESimpleEnemyState.Attack;
        }
    }
}