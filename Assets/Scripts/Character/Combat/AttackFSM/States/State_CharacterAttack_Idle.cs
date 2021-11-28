using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterAttack_Idle : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private AnimationController_Character _characterAnimationController = null;

        [SerializeField] private float _fixedTransitionDuration = 0.1f;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.Idle;
        }
        
        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            _characterAnimationController.TryCrossFadeAnimation(
                ECharacterAnimation.ATTACK_EMPTY,
                _fixedTransitionDuration,
                AnimationController_Character.ATTACK_LAYER,
                false);
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}