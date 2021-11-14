using BLUE.FSMSystem;
using BLUE.ProjectX.Character;
using UnityEngine;

namespace Character.Combat.AttackFSM.States
{
    public class State_CharacterAttack_Idle : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private AnimationController_Character _characterAnimationController = null;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.Idle;
        }
        
        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            _characterAnimationController.TryCrossFadeAnimation(ECharacterAnimation.ATTACK_EMPTY, 0.1f, 1, false);
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}