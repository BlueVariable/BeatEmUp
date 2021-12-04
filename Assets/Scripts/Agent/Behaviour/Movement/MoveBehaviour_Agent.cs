using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace BLUE.ProjectX.Enemy
{
    public class MoveBehaviour_Agent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent = null;

        [SerializeField] private float _distanceReachedThreshold = 0.1f;

        [SerializeField] private int _randomPointQueryCount = 5;
        
        private IEnumerator _destinationReachedRoutine;

        private Vector3 _lastMoveTarget;

        private Action _onDestinationReachedCallback;
        
        public void Move(
            Vector3 destination, 
            Action onDestinationReachedCallback = null)
        {
            _lastMoveTarget = destination;

            _onDestinationReachedCallback = onDestinationReachedCallback;
            
            _agent.isStopped = false;

            _agent.destination = _lastMoveTarget;
            
            StartCheckReachedDestinationRoutine();
        }

        public void MoveToRandomPoint(
            float minRadius, 
            float maxRadius, 
            float maxDistance = 0,
            Action onDestinationReachedCallback = null)
        {
            Vector3 randomDestination = _agent.transform.position;
            
            for (int i = 0; i < _randomPointQueryCount; i++)
            {
                if (!TryGetRandomDestination(out randomDestination, minRadius, maxRadius, maxDistance))
                {
                    continue;
                }
                
                Move(randomDestination, onDestinationReachedCallback);

                return;
            }
            
            onDestinationReachedCallback?.Invoke();
        }

        public void Stop()
        {
            _agent.isStopped = true;
            
            StopCheckReachedDestinationRoutine();
        }
        
        private bool TryGetRandomDestination(out Vector3 randomDestination, float minRadius, float maxRadius, float maxDistance = 0)
        {
            randomDestination = _agent.transform.position;

            float radius = Random.Range(minRadius, maxRadius);

            Vector3 randPosition = Random.onUnitSphere * radius;
            
            randPosition += _agent.transform.position;
            
            randPosition.y = _agent.transform.position.y;
            
            if (maxDistance == 0)
                maxDistance = _agent.height * 2.0f;

            if (NavMesh.SamplePosition(randPosition, out NavMeshHit navHit, maxDistance, NavMesh.AllAreas))
            {
                randomDestination = navHit.position;
                
                return true;
            }
            
            return false;
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
            
            _onDestinationReachedCallback?.Invoke();
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