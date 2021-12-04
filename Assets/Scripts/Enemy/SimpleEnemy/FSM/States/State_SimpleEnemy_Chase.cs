using System;
using BLUE.ActionSystem;
using BLUE.FSMSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class State_SimpleEnemy_Chase : StateBase<ESimpleEnemyState, FSMTransitionMessage_SimpleEnemy_Chase>
    {
        [SerializeField] private FSM_SimpleEnemy _fsm = null;
        
        [SerializeField] private Trigger_LayerBase[] _chaseEnterRequestors = new Trigger_LayerBase[0];

        [SerializeField] private ActionExecutor _chaseActions = null;
        
        public Transform LastChasedTarget { get; private set; }
        
        public override ESimpleEnemyState GetStateType()
        {
            return ESimpleEnemyState.Chase;
        }
        
        protected override void EnterStateCustomActions(FSMTransitionMessage_SimpleEnemy_Chase transitionMessage)
        {
            LastChasedTarget = transitionMessage.ChasedTarget;
            
            _chaseActions.Execute();
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _chaseActions.Interrupt();
            
            base.ExitStateCustomActions();
        }

        private void Awake()
        {
            Array.ForEach(_chaseEnterRequestors, i => i.OnTriggered += OnTriggered);
        }

        private void OnDestroy()
        {
            Array.ForEach(_chaseEnterRequestors, i => i.OnTriggered -= OnTriggered);
        }

        private void OnTriggered(TriggerBase triggerBase)
        {
            _fsm.TryChangeState(
                ESimpleEnemyState.Chase, 
                new FSMTransitionMessage_SimpleEnemy_Chase(((Trigger_LayerBase) triggerBase).LastValidObject.transform));
        }
    }
}