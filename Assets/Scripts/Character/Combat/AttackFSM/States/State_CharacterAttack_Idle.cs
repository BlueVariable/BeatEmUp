using BLUE.FSMSystem;
using BLUE.ProjectX.Character;
using UnityEngine;

namespace Character.Combat.AttackFSM.States
{
    public class State_CharacterAttack_Idle : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private AnimationController_Character _characterAnimationContorller = null;

        [SerializeField] private float _transitionDuration = 0.25f;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.Idle;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            _characterAnimationContorller.TryCrossFadeAnimation(ECharacterAnimation.ATTACK_EMPTY, _transitionDuration, 1);
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}