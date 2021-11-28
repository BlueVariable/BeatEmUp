using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_AttackIdle : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterAttack _characterAttackFSM = null;

        [SerializeField] private AnimationController_Character _animationController = null;
        
        [SerializeField] private float _animationTransitionDuration = 0.1f;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.AttackIdle;
        }

        public override bool CheckStatus()
        {
            _characterAttackFSM.TryGetCurStateType(out ECharacterAttackState curState);

            return curState == ECharacterAttackState.Idle;
        }

        protected override void StatusAppearedCustomActions()
        {
            _animationController.TryCrossFadeAnimation(
                ECharacterAnimation.ATTACK_EMPTY, 
                _animationTransitionDuration,
                AnimationController_Character.ATTACK_LAYER,
                false);
            
            base.StatusAppearedCustomActions();
        }
    }
}