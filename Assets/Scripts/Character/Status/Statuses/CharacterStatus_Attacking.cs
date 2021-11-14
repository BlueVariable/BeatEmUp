using Character.Combat.AttackFSM;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_Attacking : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterAttack _characterAttackFSM = null;
        
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
    }
}