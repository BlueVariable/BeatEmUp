using System;
using BLUE.ActionSystem;
using BLUE.AnimationSystem;
using BLUE.Extensions;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class Action_SimpleEnemy_WaitForAnimationCallback : InterruptableActionBase
    {
        [SerializeField] private AnimationController_SimpleEnemy _animController = null;

        [SerializeField] private ESimpleEnemyAnimation _animType = ESimpleEnemyAnimation.ATTACK;

        [SerializeField] private float _attackImpactNormTime = 0.4f;

        private AnimationCallback _animCallback = null;

        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            RemoveCallback();
            
            string fullPath = _animController.GetFullPathOfState(_animType,
                AnimationController_SimpleEnemy.BASE_LAYER);

            StateMachineBehaviour_AnimationEvent eventBehaviour =
                _animController.Animator.TryGetBehaviour<StateMachineBehaviour_AnimationEvent>(fullPath);

            void onAttackImpactRequested()
            {
                RemoveCallback();

                OnCompleted?.Invoke(this);
            }

            _animCallback = new AnimationCallback(onAttackImpactRequested, _attackImpactNormTime);

            eventBehaviour.AddCallback(_animCallback);
        }

        public override void Interrupt()
        {
            RemoveCallback();
        }

        private void RemoveCallback()
        {
            string fullPath = _animController.GetFullPathOfState(_animType,
                AnimationController_SimpleEnemy.BASE_LAYER);
            
            StateMachineBehaviour_AnimationEvent eventBehaviour =
                _animController.Animator.TryGetBehaviour<StateMachineBehaviour_AnimationEvent>(fullPath);

            eventBehaviour.RemoveCallback(_animCallback);
        }
    }
}