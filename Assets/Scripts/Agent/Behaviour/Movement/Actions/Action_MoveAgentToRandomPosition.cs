using System;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class Action_MoveAgentToRandomPosition : InterruptableActionBase
    {
        [SerializeField] private MoveBehaviour_Agent _moveBehaviour = null;

        [Header("Random Position Settings")]
        [SerializeField] private float _minRadius = 0.5f;
        
        [SerializeField] private float _maxRadius = 3.0f;
        
        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        public override void Execute()
        {
            _moveBehaviour.MoveToRandomPoint(
                _minRadius,
                _maxRadius,
                onDestinationReachedCallback: () => OnCompleted?.Invoke(this));
        }

        public override void Interrupt()
        {
            _moveBehaviour.Stop();
        }
    }
}