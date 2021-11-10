using System.Collections.Generic;
using BLUE.FSMSystem;

namespace Character.Combat.AttackFSM
{
    public enum ECharacterAttackState
    {
        None = 0,
        Idle = 1,
        MeleeAttack = 2,
        RangedAttack = 3
    }
    
    public class FSMTransitionMessage_CharacterAttack : FSMTransitionMessageBase { }
    
    public class FSM_CharacterAttack : FSMBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack, FSMTransition_CharacterAttack>
    {
        protected override List<FSMTransition_CharacterAttack> GetInitialPossibleTransitions()
        {
            return new List<FSMTransition_CharacterAttack>()
            {
                new FSMTransition_CharacterAttack(ECharacterAttackState.Idle, ECharacterAttackState.MeleeAttack),
                new FSMTransition_CharacterAttack(ECharacterAttackState.Idle, ECharacterAttackState.RangedAttack),
                
                new FSMTransition_CharacterAttack(ECharacterAttackState.MeleeAttack, ECharacterAttackState.Idle),
                new FSMTransition_CharacterAttack(ECharacterAttackState.MeleeAttack, ECharacterAttackState.RangedAttack),
                
                
                new FSMTransition_CharacterAttack(ECharacterAttackState.RangedAttack, ECharacterAttackState.Idle),
                new FSMTransition_CharacterAttack(ECharacterAttackState.RangedAttack, ECharacterAttackState.MeleeAttack),
            };
        }
    }
}