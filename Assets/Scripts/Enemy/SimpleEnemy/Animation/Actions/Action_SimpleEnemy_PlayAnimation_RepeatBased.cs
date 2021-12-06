using System;
using System.Collections;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class Action_SimpleEnemy_PlayAnimation_RepeatBased : InterruptableActionBase
    {
        [SerializeField] private AnimationController_SimpleEnemy _animationController = null;
        
        [SerializeField] private ESimpleEnemyAnimation[] _animations = Array.Empty<ESimpleEnemyAnimation>();

        [Header("Repeat Settings")]
        [SerializeField] private int _repeatCount = 1;

        private IEnumerator _animationRoutine;

        private bool _animPlayed = false;
        
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
            int repeatCount = 0;

            while (repeatCount < _repeatCount)
            {
                _animPlayed = false;

                _animationController.OnAnimationPeriodCompleted += OnAnimPeriodCompleted;

                _animationController
                    .TryCrossFadeAnimation(_animations[UnityEngine.Random.Range(0, _animations.Length)], 
                        ignoreIfAlreadyPlaying:false);

                yield return new WaitUntil(() => _animPlayed);
                
                repeatCount++;
            }

            OnCompleted?.Invoke(this);
        }
        
        private void OnAnimPeriodCompleted(ESimpleEnemyAnimation animType, int layerIndex, int curLoop)
        {
            _animationController.OnAnimationPeriodCompleted -= OnAnimPeriodCompleted;

            _animPlayed = true;
        }
    }
}