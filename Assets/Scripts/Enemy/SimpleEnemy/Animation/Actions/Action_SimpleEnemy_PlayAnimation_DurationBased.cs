using System;
using System.Collections;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class Action_SimpleEnemy_PlayAnimation_DurationBased : InterruptableActionBase
    {
        [SerializeField] private AnimationController_SimpleEnemy _animationController = null;
        
        [SerializeField] private ESimpleEnemyAnimation[] _animations = Array.Empty<ESimpleEnemyAnimation>();
         
        [Header("Duration Settings")]
        [SerializeField] private float _minDuration = 0f;
        [SerializeField] private float _maxDuration = 0f;

        private IEnumerator _animationRoutine;

        private Action _animPeriodCompletedCallback;

        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion
        
        public override void Execute()
        {
            StartAnimationRoutine();
        }
        
        public override void Interrupt()
        {
            _animationController.OnAnimationPeriodCompleted -= OnAnimPeriodCompleted;
            
            StopAnimationRoutine();
        }

        private void OnDestroy()
        {
            Interrupt();
        }

        private void StartAnimationRoutine()
        {
            StopAnimationRoutine();

            _animationRoutine = AnimationProgress();

            StartCoroutine(_animationRoutine);
        }

        private void StopAnimationRoutine()
        {
            if (_animationRoutine != null)
            {
                StopCoroutine(_animationRoutine);
            }
        }

        private IEnumerator AnimationProgress()
        {
            float timePassed = 0;

            float duration = UnityEngine.Random.Range(_minDuration, _maxDuration);

            bool isPlayed = true;
            
            while (timePassed < duration)
            {
                void onPlayed()
                {
                    isPlayed = true;
                }
                
                if (isPlayed)
                {
                    isPlayed = false;
                    
                    PlayRandomAnimation(onPlayed);
                }
                
                timePassed += Time.deltaTime;
                
                yield return Time.deltaTime;
            }
            
            OnCompleted?.Invoke(this);
        }

        private void PlayRandomAnimation(Action onCompleted)
        {
            _animPeriodCompletedCallback = onCompleted;
            
            _animationController.OnAnimationPeriodCompleted += OnAnimPeriodCompleted;

            _animationController
                .TryCrossFadeAnimation(_animations[UnityEngine.Random.Range(0, _animations.Length)], 
                    ignoreIfAlreadyPlaying:false);
        }

        private void OnAnimPeriodCompleted(ESimpleEnemyAnimation animType, int layerIndex, int curLoop)
        {
            _animationController.OnAnimationPeriodCompleted -= OnAnimPeriodCompleted;
                
            _animPeriodCompletedCallback?.Invoke();
        }
    }
}