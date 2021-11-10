using BLUE.FSMSystem;

namespace Character.Combat.AttackFSM.States
{
    public class State_CharacterAttack_Idle : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.Idle;
        }
    }
}