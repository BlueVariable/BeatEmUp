using System;
using BLUE.FSMSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class State_SimpleEnemy_Death : StateBase<ESimpleEnemyState, FSMTransitionMessage_SimpleEnemy>
    {
        [SerializeField] private GameObject _enemy = null;
        
        [SerializeField] private FSM_SimpleEnemy _fsm = null;
        
        [SerializeField] private TriggerBase[] _deathEnterRequestors = new TriggerBase[0];

        public override ESimpleEnemyState GetStateType()
        {
            return ESimpleEnemyState.Death;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_SimpleEnemy transitionMessage)
        {
            Destroy(_enemy);
            
            base.EnterStateCustomActions(transitionMessage);
        }
        
        private void Awake()
        {
            Array.ForEach(_deathEnterRequestors, i => i.OnTriggered += OnTriggered);
        }

        private void OnDestroy()
        {
            Array.ForEach(_deathEnterRequestors, i => i.OnTriggered -= OnTriggered);
        }

        private void OnTriggered(TriggerBase triggerBase)
        {
            _fsm.TryChangeState(ESimpleEnemyState.Death, new FSMTransitionMessage_SimpleEnemy());
        }
    }
}
