using BLUE.DamageSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_Death : CharacterStatusBase
    {
        [SerializeField] private Damagable _characterDamagable = null;

        [SerializeField] private AnimationController_Character _animationController = null;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.Death;
        }

        public override bool CheckStatus()
        {
            return _characterDamagable.IsDead;
        }

        protected override void StatusAppearedCustomActions()
        {
            _animationController.TryCrossFadeAnimation(
                ECharacterAnimation.DEATH,
                layerIndex:1);
            
            base.StatusAppearedCustomActions();
        }
    }
}