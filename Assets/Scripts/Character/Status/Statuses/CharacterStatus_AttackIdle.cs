using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_AttackIdle : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterAttack _characterAttackFSM = null;

        [SerializeField] private AnimationController_Character _animationController = null;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.AttackIdle;
        }

        public override bool CheckStatus()
        {
            _characterAttackFSM.TryGetCurStateType(out ECharacterAttackState curState);

            return curState == ECharacterAttackState.Idle;
        }

        protected override void AppearStatusCustomActions()
        {
            _animationController.TryCrossFadeAnimation(
                ECharacterAnimation.ATTACK_EMPTY, 
                0.1f,
                AnimationController_Character.ATTACK_LAYER,
                false);
            
            base.AppearStatusCustomActions();
        }
    }
}