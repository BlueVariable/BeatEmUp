using System.Collections;
using UnityEngine;

namespace BLUE.ProjectX.Common
{
    public class RotationBehaviour : RotationBehaviourBase
    {
        [SerializeField] private Transform _targetTransform = null;
        
        [SerializeField] private float _rotationSpeed = 5f;

        private IEnumerator _rotationRoutine;
        
        public override void Rotate(Vector3 targetRotation)
        {
            StartRotationRoutine(targetRotation);
        }

        private void StartRotationRoutine(Vector3 targetRotation)
        {
            StopRotationRoutine();

            _rotationRoutine = RotationProgress(targetRotation);

            StartCoroutine(_rotationRoutine);
        }

        private void StopRotationRoutine()
        {
            if (_rotationRoutine != null)
            {
                StopCoroutine(_rotationRoutine);

                _rotationRoutine = null;
            }
        }

        private IEnumerator RotationProgress(Vector3 targetRotation)
        {
            Quaternion initialQuaternion = _targetTransform.localRotation;
            
            Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
            
            while (Quaternion.Angle(_targetTransform.localRotation, targetQuaternion) > 0.05f)
            {
                _targetTransform.rotation = Quaternion.Lerp(initialQuaternion, targetQuaternion, _rotationSpeed * Time.deltaTime);
                
                yield return null;
            }
        }
    }
}