using System;
using BLUE.ActionSystem;
using BLUE.FSMSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class State_SimpleEnemy_Idle : StateBase<ESimpleEnemyState, FSMTransitionMessage_SimpleEnemy>
    {
        [SerializeField] private FSM_SimpleEnemy _fsm = null;
        
        [SerializeField] private TriggerBase[] _idleEnterRequestors = new TriggerBase[0];

        [SerializeField] private ActionExecutor _idleActions = null;

        public override ESimpleEnemyState GetStateType()
        {
            return ESimpleEnemyState.Idle;
        }
        
        protected override void EnterStateCustomActions(FSMTransitionMessage_SimpleEnemy transitionMessage)
        {
            _idleActions.Execute();
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _idleActions.Interrupt();
            
            base.ExitStateCustomActions();
        }

        private void Awake()
        {
            Array.ForEach(_idleEnterRequestors, i => i.OnTriggered += OnTriggered);
        }

        private void OnDestroy()
        {
            Array.ForEach(_idleEnterRequestors, i => i.OnTriggered -= OnTriggered);
        }

        private void OnTriggered(TriggerBase triggerBase)
        {
            _fsm.TryChangeState(ESimpleEnemyState.Idle, new FSMTransitionMessage_SimpleEnemy());
        }
    }
}