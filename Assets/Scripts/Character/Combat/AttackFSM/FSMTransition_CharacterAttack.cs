using BLUE.FSMSystem;

namespace Character.Combat.AttackFSM
{
    public class FSMTransition_CharacterAttack : FSMTransitionBase<ECharacterAttackState>
    {
        public FSMTransition_CharacterAttack(ECharacterAttackState initialStateType, ECharacterAttackState targetStateType) 
            : base(initialStateType, targetStateType)
        {
        }
    }
}