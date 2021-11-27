using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_Anticipation : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterAttack _characterAttackFSM = null;

        [SerializeField] private AnimationController_Character _characterAnimationController = null;

        [SerializeField] private State_CharacterAttack_Anticipation _anticipationState = null;
        
        [SerializeField] private float _animationTransitionDuration = 0.25f;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.Anticipation;
        }

        public override bool CheckStatus()
        {
            _characterAttackFSM.TryGetCurStateType(out ECharacterAttackState curAttackState);

            return curAttackState == ECharacterAttackState.Anticipation;
        }

        protected override void StatusAppearedCustomActions()
        {
            _characterAttackFSM.TryGetCurStateType(out ECharacterAttackState curAttackState);

            if (curAttackState == ECharacterAttackState.Anticipation)
            {
                if(_anticipationState.LastTransitionMessage.AttackType == EAttackType.Melee)
                {
                    _characterAnimationController.TryCrossFadeAnimation(ECharacterAnimation.MELEE_ATTACK,
                        _animationTransitionDuration,
                        AnimationController_Character.ATTACK_LAYER,
                        false);
                }
                
                if(_anticipationState.LastTransitionMessage.AttackType == EAttackType.Ranged)
                {
                    _characterAnimationController.TryCrossFadeAnimation(ECharacterAnimation.RANGED_ATTACK,
                        _animationTransitionDuration, 
                        AnimationController_Character.ATTACK_LAYER,
                        false);
                }
            }

            base.StatusAppearedCustomActions();
        }

        protected override void StatusDisappearedCustomActions()
        {
            _characterAttackFSM.TryGetCurStateType(out ECharacterAttackState curAttackState);

            if (curAttackState != ECharacterAttackState.MeleeAttack &&
                curAttackState != ECharacterAttackState.RangedAttack)
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