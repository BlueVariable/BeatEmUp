using BLUE.FSMSystem;

namespace Character.Combat.AttackFSM.States
{
    public class State_CharacterAttack_MeleeAttack : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.MeleeAttack;
        }
    }
}