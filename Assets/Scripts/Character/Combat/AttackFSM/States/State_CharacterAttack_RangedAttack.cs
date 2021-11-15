using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character.States
{
    public class State_CharacterAttack_RangedAttack : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.RangedAttack;
        }
    }
}