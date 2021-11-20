using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_Attacking : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterAttack _characterAttackFSM = null;

        [SerializeField] private AnimationController_Character _characterAnimationController = null;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.Attacking;
        }

        public override bool CheckStatus()
        {
            _characterAttackFSM.TryGetCurStateType(out ECharacterAttackState curAttackState);

            return curAttackState == ECharacterAttackState.MeleeAttack ||
                   curAttackState == ECharacterAttackState.RangedAttack;
        }
        
        protected override void DisappearedStatusCustomActions()
        {
            _characterAttackFSM.TryGetCurStateType(out ECharacterAttackState curAttackState);

            if (curAttackState != ECharacterAttackState.Recovery)
            {
                _characterAnimationController.TryCrossFadeAnimation(ECharacterAnimation.ATTACK_EMPTY,
                    0.1f, 
                    AnimationController_Character.ATTACK_LAYER,
                    false);
            }
            
            base.DisappearedStatusCustomActions();
        }
    }
}