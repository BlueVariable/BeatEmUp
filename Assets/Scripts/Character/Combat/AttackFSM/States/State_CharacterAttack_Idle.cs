using BLUE.FSMSystem;
using BLUE.ProjectX.Character;
using UnityEngine;

namespace BLUE.ProjectX.Character.States
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
            _characterAnimationController.TryCrossFadeAnimation(
                ECharacterAnimation.ATTACK_EMPTY,
                0.1f,
                AnimationController_Character.ATTACK_LAYER,
                false);
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}