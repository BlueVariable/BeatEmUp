using System;
using BLUE.ActionSystem;
using BLUE.FSMSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class State_SimpleEnemy_Attack : StateBase<ESimpleEnemyState, FSMTransitionMessage_SimpleEnemy_Attack>
    {
        [SerializeField] private FSM_SimpleEnemy _fsm = null;

        [SerializeField] private TriggerBase[] _attackEnterRequestors = new TriggerBase[0];

        [SerializeField] private ActionExecutor _attackActions = null;

        public Transform AttackTarget { get; private set; }
        
        public override ESimpleEnemyState GetStateType()
        {
            return ESimpleEnemyState.Attack;
        }
        
        protected override void EnterStateCustomActions(FSMTransitionMessage_SimpleEnemy_Attack transitionMessage)
        {
            AttackTarget = transitionMessage.AttackTarget;
            
            _attackActions.Execute();
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _attackActions.Interrupt();
            
            base.ExitStateCustomActions();
        }

        private void Awake()
        {
            Array.ForEach(_attackEnterRequestors, i => i.OnTriggered += OnTriggered);
        }

        private void OnDestroy()
        {
            Array.ForEach(_attackEnterRequestors, i => i.OnTriggered -= OnTriggered);
        }

        private void OnTriggered(TriggerBase triggerBase)
        {
            _fsm.TryChangeState(
                ESimpleEnemyState.Attack, 
                new FSMTransitionMessage_SimpleEnemy_Attack(((Trigger_LayerBase) triggerBase).LastValidObject.transform));
        }
    }
}