using System;
using BLUE.ActionSystem;
using BLUE.FSMSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class State_SimpleEnemy_Death : StateBase<ESimpleEnemyState, FSMTransitionMessage_SimpleEnemy>
    {
        [SerializeField] private FSM_SimpleEnemy _fsm = null;
        
        [SerializeField] private TriggerBase[] _deathEnterRequestors = new TriggerBase[0];

        [SerializeField] private ActionExecutor _deathActions = null;

        public override ESimpleEnemyState GetStateType()
        {
            return ESimpleEnemyState.Death;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_SimpleEnemy transitionMessage)
        {
            _deathActions.Execute();

            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _deathActions.Interrupt();
            
            base.ExitStateCustomActions();
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
