using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_Recovery : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterAttack _characterAttackFSM = null;

        [SerializeField] private AnimationController_Character _characterAnimationController = null;

        [SerializeField] private float _transitionFixedDuration = 0.25f;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.Recovery;
        }

        public override bool CheckStatus()
        {
            _characterAttackFSM.TryGetCurStateType(out ECharacterAttackState curAttackState);

            return curAttackState == ECharacterAttackState.Recovery;
        }

        protected override void StatusDisappearedCustomActions()
        {
            _characterAnimationController.TryCrossFadeAnimation(
                ECharacterAnimation.ATTACK_EMPTY,
                _transitionFixedDuration,
                AnimationController_Character.ATTACK_LAYER,
                false);
            
            base.StatusDisappearedCustomActions();
        }
    }
}