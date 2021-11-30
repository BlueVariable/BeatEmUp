using BLUE.FSMSystem;

namespace BLUE.ProjectX.Enemy
{
    public class State_SimpleEnemy_Idle : StateBase<ESimpleEnemyState, FSMTransitionMessage_SimpleEnemy>
    {
        public override ESimpleEnemyState GetStateType()
        {
            return ESimpleEnemyState.Idle;
        }
    }
}