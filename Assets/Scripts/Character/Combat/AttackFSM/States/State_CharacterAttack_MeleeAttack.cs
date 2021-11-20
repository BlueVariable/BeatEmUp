using BLUE.FSMSystem;
using BLUE.Helpers;
using Extensions;
using UnityEngine;

namespace BLUE.ProjectX.Character.States
{
    public class State_CharacterAttack_MeleeAttack : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private FSM_CharacterAttack _attackFSM = null;

        [SerializeField] private AnimationController_Character _characterAnimController = null;

        private FSMTransitionMessage_CharacterAttack _lastTransitionMessage = null;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.MeleeAttack;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            _lastTransitionMessage = transitionMessage;
            
            string fullPath = _characterAnimController.GetFullPathOfState(ECharacterAnimation.MELEE_ATTACK,
                AnimationController_Character.ATTACK_LAYER);

            AnimationStateEventBehaviour eventBehaviour =
                _characterAnimController.Animator.TryGetBehaviour<AnimationStateEventBehaviour>(fullPath);
            
            eventBehaviour.AddCallback(new CharacterAnimationCallback(OnAttackCompleted, transitionMessage.AttackSettings.AttackTime));
            
            base.EnterStateCustomActions(transitionMessage);
        }

        private void OnAttackCompleted()
        {
            _attackFSM.TryChangeState(ECharacterAttackState.Recovery, _lastTransitionMessage);
        }
    }
}