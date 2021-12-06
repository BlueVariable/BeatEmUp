using System;
using BLUE.ActionSystem;
using BLUE.FSMSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class State_SimpleEnemy_Wander : StateBase<ESimpleEnemyState, FSMTransitionMessage_SimpleEnemy>
    {
        [SerializeField] private FSM_SimpleEnemy _fsm = null;
        
        [SerializeField] private TriggerBase[] _wanderEnterRequestors = new TriggerBase[0];

        [SerializeField] private ActionExecutor _wanderActions = null;
        
        public override ESimpleEnemyState GetStateType()
        {
            return ESimpleEnemyState.Wander;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_SimpleEnemy transitionMessage)
        {
            _wanderActions.OnExecutionCompleted += OnExecutionCompleted;
            
            _wanderActions.Execute();
            
            base.EnterStateCustomActions(transitionMessage);
        }
        
        protected override void ExitStateCustomActions()
        {
            _wanderActions.OnExecutionCompleted -= OnExecutionCompleted;

            _wanderActions.Interrupt();
            
            base.ExitStateCustomActions();
        }

        private void OnExecutionCompleted()
        {
            _fsm.TryChangeState(ESimpleEnemyState.Idle);
        }

        private void Awake()
        {
            Array.ForEach(_wanderEnterRequestors, i => i.OnTriggered += OnTriggered);
        }

        private void OnDestroy()
        {
            Array.ForEach(_wanderEnterRequestors, i => i.OnTriggered -= OnTriggered);
        }

        private void OnTriggered(TriggerBase triggerBase)
        {
            _fsm.TryChangeState(ESimpleEnemyState.Wander, new FSMTransitionMessage_SimpleEnemy());
        }
    }
}