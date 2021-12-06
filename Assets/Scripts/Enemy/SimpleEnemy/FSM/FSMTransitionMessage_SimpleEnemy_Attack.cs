using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class FSMTransitionMessage_SimpleEnemy_Attack : FSMTransitionMessage_SimpleEnemy
    {
        public Transform AttackTarget { get; private set; }

        public FSMTransitionMessage_SimpleEnemy_Attack(Transform attackTarget)
        {
            AttackTarget = attackTarget;
        }
    }
}