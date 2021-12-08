using System;
using BLUE.ActionSystem;
using BLUE.AnimationSystem;
using BLUE.Extensions;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class Action_SimpleEnemy_WaitForAnimationFinish : InterruptableActionBase
    {
        [SerializeField] private AnimationController_SimpleEnemy _animController = null;

        [SerializeField] private ESimpleEnemyAnimation _animType = ESimpleEnemyAnimation.ATTACK;

        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            RegisterToStateExit();
        }

        public override void Interrupt()
        {
            UnregisterFromStateExit();
        }

        private void RegisterToStateExit()
        {
            string fullPath = _animController.GetFullPathOfState(_animType,
                AnimationController_SimpleEnemy.BASE_LAYER);

            StateMachineBehaviour_AnimationEvent eventBehaviour =
                _animController.Animator.TryGetBehaviour<StateMachineBehaviour_AnimationEvent>(fullPath);

            eventBehaviour.OnStateExitEvent += OnStateExitEvent;
        }

        private void UnregisterFromStateExit()
        {
            string fullPath = _animController.GetFullPathOfState(_animType,
                AnimationController_SimpleEnemy.BASE_LAYER);

            StateMachineBehaviour_AnimationEvent eventBehaviour =
                _animController.Animator.TryGetBehaviour<StateMachineBehaviour_AnimationEvent>(fullPath);

            eventBehaviour.OnStateExitEvent -= OnStateExitEvent;
        }

        private void OnStateExitEvent()
        {
            OnCompleted?.Invoke(this);
        }
    }
}
