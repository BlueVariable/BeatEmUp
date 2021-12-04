using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class FSMTransitionMessage_SimpleEnemy_Chase : FSMTransitionMessage_SimpleEnemy
    {
        public Transform ChasedTarget { get; private set; }

        public FSMTransitionMessage_SimpleEnemy_Chase(Transform chasedTarget)
        {
            ChasedTarget = chasedTarget;
        }
    }
}