using BLUE.AnimationSystem;
using BLUE.Extensions;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterAttack_Recovery : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private FSM_CharacterAttack _attackFSM = null;
        
        [SerializeField] private AnimationController_Character _characterAnimationController = null;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.Recovery;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            ECharacterAnimation targetAnimType = transitionMessage.AttackType == EAttackType.Melee
                ? ECharacterAnimation.MELEE_ATTACK
                : ECharacterAnimation.RANGED_ATTACK;

            string fullPath =
                _characterAnimationController.GetFullPathOfState(targetAnimType,
                    AnimationController_Character.ATTACK_LAYER);

            StateMachineBehaviour_AnimationEvent eventBehaviour = _characterAnimationController
                .Animator.TryGetBehaviour<StateMachineBehaviour_AnimationEvent>(fullPath);
            
            eventBehaviour.AddCallback(
                new AnimationCallback(
                    OnRecoveryCompleted,
                    transitionMessage.AttackSettings.RecoveryTime));
            
            base.EnterStateCustomActions(transitionMessage);
        }

        private void OnRecoveryCompleted()
        {
            _attackFSM.TryChangeState(ECharacterAttackState.Idle);
        }
    }
}