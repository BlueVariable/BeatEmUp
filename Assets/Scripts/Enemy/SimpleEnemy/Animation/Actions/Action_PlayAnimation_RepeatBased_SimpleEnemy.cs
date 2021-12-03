using System;
using System.Collections;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class Action_PlayAnimation_RepeatBased_SimpleEnemy : InterruptableActionBase
    {
        [SerializeField] private AnimationController_SimpleEnemy _animationController = null;
        
        [SerializeField] private ESimpleEnemyAnimation[] _animations = new ESimpleEnemyAnimation[0];

        [Header("Repeat Settings")]
        [SerializeField] private int _repeatCount = 1;

        private IEnumerator _animationRoutine;
        
        public override Action<InterruptableActionBase> OnCompleted { get; set; }
        
        public override void Execute()
        {
            StartAnimationRoutine();
        }
        
        public override void Interrupt()
        {
            StopAnimationRoutine();
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
            int repeatCount = 0;

            while (repeatCount < _repeatCount)
            {
                bool onPlayed = false;
                
                void onAnimationPeriodCompleted(ESimpleEnemyAnimation animType, int layerIndex, int curLoop)
                {
                    _animationController.OnAnimationPeriodCompleted -= onAnimationPeriodCompleted;

                    onPlayed = true;
                }
                
                _animationController.OnAnimationPeriodCompleted += onAnimationPeriodCompleted;

                _animationController
                    .TryCrossFadeAnimation(_animations[UnityEngine.Random.Range(0, _animations.Length)]);

                yield return new WaitUntil(() => onPlayed);
                
                repeatCount++;
            }
            
            OnCompleted?.Invoke(this);
        }
    }
}