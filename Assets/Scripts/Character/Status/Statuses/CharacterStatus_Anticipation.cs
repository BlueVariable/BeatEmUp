using Character.Combat.AttackFSM;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_Anticipation : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterAttack _characterAttackFSM = null;

        [SerializeField] private AnimationController_Character _characterAnimationController = null;

        [SerializeField] private float _animationTransitionDuration = 0.25f;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.Anticipation;
        }

        public override bool CheckStatus()
        {
            _characterAttackFSM.TryGetCurStateType(out ECharacterAttackState curAttackState);

            return curAttackState == ECharacterAttackState.PreMeleeAttack ||
                   curAttackState == ECharacterAttackState.PreRangedAttack;
        }

        protected override void AppearStatusCustomActions()
        {
            _characterAnimationController.TryCrossFadeAnimation(ECharacterAnimation.MELEE_ATTACK,
                _animationTransitionDuration, 1, false);
            
            base.AppearStatusCustomActions();
        }
    }
}