using BLUE.AnimationSystem;
using BLUE.Extensions;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterAttack_RangedAttack : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private FSM_CharacterAttack _attackFSM = null;
        
        [SerializeField] private AnimationController_Character _characterAnimController = null;
        
        private FSMTransitionMessage_CharacterAttack _lastTransitionMessage = null;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.RangedAttack;
        }
        
        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            _lastTransitionMessage = transitionMessage;
            
            string fullPath = _characterAnimController.GetFullPathOfState(ECharacterAnimation.RANGED_ATTACK,
                AnimationController_Character.ATTACK_LAYER);

            StateMachineBehaviour_AnimationEvent eventBehaviour =
                _characterAnimController.Animator.TryGetBehaviour<StateMachineBehaviour_AnimationEvent>(fullPath);
            
            eventBehaviour.AddCallback(new AnimationCallback(OnAttackCompleted, transitionMessage.AttackSettings.AttackTime));
            
            base.EnterStateCustomActions(transitionMessage);
        }
        
        private void OnAttackCompleted()
        {
            _attackFSM.TryChangeState(ECharacterAttackState.Recovery, _lastTransitionMessage);
        }
    }
}