using System;
using System.Collections;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class Action_SimpleEnemy_ChaseTarget : InterruptableActionBase
    {
        [SerializeField] private MoveBehaviour_Agent _moveBehaviour = null;

        [SerializeField] private State_SimpleEnemy_Chase _chaseState = null;
        
        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        private IEnumerator _chaseRoutine;
        
        public override void Execute()
        {
            StartChaseRoutine();
        }

        public override void Interrupt()
        {
            _moveBehaviour.Stop();

            StopChaseRoutine();
        }
        
        private void StartChaseRoutine()
        {
            StopChaseRoutine();
            
            _chaseRoutine = ChaseProgress();

            StartCoroutine(_chaseRoutine);
        }

        private void StopChaseRoutine()
        {
            if (_chaseRoutine != null)
            {
                StopCoroutine(_chaseRoutine);
            }
        }
        
        private IEnumerator ChaseProgress()
        {
            while (true)
            {
                _moveBehaviour.Move(_chaseState.LastChasedTarget.position);

                yield return null;
            }
        }
    }
}