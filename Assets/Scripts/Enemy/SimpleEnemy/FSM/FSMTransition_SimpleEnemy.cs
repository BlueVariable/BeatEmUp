using BLUE.FSMSystem;

namespace BLUE.ProjectX.Enemy
{
    public class FSMTransition_SimpleEnemy : FSMTransitionBase<ESimpleEnemyState>
    {
        public FSMTransition_SimpleEnemy(ESimpleEnemyState initialStateType, ESimpleEnemyState targetStateType) 
            : base(initialStateType, targetStateType)
        {
        }
    }
}