using System;
using BLUE.ActionSystem;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class Action_SimpleEnemy_RotateTowardsAttackTarget : InterruptableActionBase
    {
        [SerializeField] private RotationBehaviour_LerpAngle _rotBehaviour = null;

        [SerializeField] private RotationBehaviour_LerpAngle.RotationSettings _rotationSettings = null;
        
        [SerializeField] private State_SimpleEnemy_Attack _attackState = null;
        
        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            _rotBehaviour.StartRotation(_rotationSettings, _attackState.AttackTarget, OnRotationSatisfied);
        }

        public override void Interrupt()
        {
            _rotBehaviour.StopRotation();
        }

        private void OnDestroy()
        {
            Interrupt();
        }

        private void OnRotationSatisfied(bool didSatisfy)
        {
            OnCompleted?.Invoke(this);
        }
    }
}
