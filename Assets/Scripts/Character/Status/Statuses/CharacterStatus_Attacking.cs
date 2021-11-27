using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_Attacking : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterAttack _characterAttackFSM = null;

        [SerializeField] private AnimationController_Character _characterAnimationController = null;
        
        [SerializeField] private float _animationTransitionDuration = 0.1f;
        
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
        
        protected override void StatusDisappearedCustomActions()
        {
            _characterAttackFSM.TryGetCurStateType(out ECharacterAttackState curAttackState);

            if (curAttackState != ECharacterAttackState.Recovery)
            {
                _characterAnimationController.TryCrossFadeAnimation(ECharacterAnimation.ATTACK_EMPTY,
                    _animationTransitionDuration, 
                    AnimationController_Character.ATTACK_LAYER,
                    false);
            }
            
            base.StatusDisappearedCustomActions();
        }
    }
}