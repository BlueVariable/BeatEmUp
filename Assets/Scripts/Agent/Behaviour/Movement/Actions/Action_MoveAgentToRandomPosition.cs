using System;
using System.Collections;
using BLUE.ActionSystem;
using BLUE.Common;
using UnityEngine;
using UnityEngine.AI;

namespace BLUE.ProjectX.Enemy
{
    public class Action_MoveAgentToRandomPosition : InterruptableActionBase
    {
        [SerializeField] private NavMeshAgent _agent = null;

        [Header("Move Settings")]
        [SerializeField] private float _distanceReachedThreshold = 0.1f;
        
        [Header("Random Position Settings")]
        [SerializeField] private float _minRadius = 0.5f;
        
        [SerializeField] private float _maxRadius = 3.0f;
        
        private IEnumerator _destinationReachedRoutine;

        private Vector3 _lastMoveTarget;
        
        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        public override void Execute()
        {
            _lastMoveTarget = GetRandomPosition();
            
            _agent.isStopped = false;

            _agent.destination = _lastMoveTarget;
            
            StartCheckReachedDestinationRoutine();
        }

        public override void Interrupt()
        {
            _agent.isStopped = true;
            
            StopCheckReachedDestinationRoutine();
        }
        
        private Vector3 GetRandomPosition()
        {
            float radius = UnityEngine.Random.Range(_minRadius, _maxRadius);

            Vector3 randPosition = UnityEngine.Random.onUnitSphere * radius;
            
            randPosition += _agent.transform.position;
            
            randPosition.y = _agent.transform.position.y;

            NavMesh.SamplePosition(randPosition, out NavMeshHit navHit, _maxRadius, NavMesh.AllAreas);
            
            return randPosition;
        }

        private void StartCheckReachedDestinationRoutine()
        {
            StopCheckReachedDestinationRoutine();
            
            _destinationReachedRoutine = CheckReachedDestinationRoutine();

            StartCoroutine(_destinationReachedRoutine);
        }

        private void StopCheckReachedDestinationRoutine()
        {
            if (_destinationReachedRoutine != null)
            {
                StopCoroutine(_destinationReachedRoutine);
            }
        }
        
        private IEnumerator CheckReachedDestinationRoutine()
        {
            while (!IsDestinationReached())
            {
                yield return null;
            }
            
            OnCompleted?.Invoke(this);
        }
        
        private bool IsDestinationReached()
        {
            Vector3 agentPositionXZ = _agent.transform.position;
            agentPositionXZ.y = 0;

            Vector3 targetPositionXZ = _lastMoveTarget;
            targetPositionXZ.y = 0;
            
            return Vector3.Distance(agentPositionXZ, targetPositionXZ) < _distanceReachedThreshold;
        }
    }
}