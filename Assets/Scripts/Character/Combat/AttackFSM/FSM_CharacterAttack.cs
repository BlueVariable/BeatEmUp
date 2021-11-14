using System.Collections.Generic;
using BLUE.FSMSystem;

namespace Character.Combat.AttackFSM
{
    public enum ECharacterAttackState
    {
        None = 0,
        Idle = 1,
        
        PreMeleeAttack = 20,
        MeleeAttack = 21,
        PostMeleeAttack = 22,
        
        PreRangedAttack = 30,
        RangedAttack = 31,
        PostRangedAttack = 32
    }
    
    public class FSMTransitionMessage_CharacterAttack : FSMTransitionMessageBase { }
    
    public class FSM_CharacterAttack : FSMBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack, FSMTransition_CharacterAttack>
    {
        protected override List<FSMTransition_CharacterAttack> GetInitialPossibleTransitions()
        {
            return new List<FSMTransition_CharacterAttack>()
            {
                new FSMTransition_CharacterAttack(ECharacterAttackState.Idle, ECharacterAttackState.PreMeleeAttack),

                new FSMTransition_CharacterAttack(ECharacterAttackState.PreMeleeAttack, ECharacterAttackState.MeleeAttack),

                new FSMTransition_CharacterAttack(ECharacterAttackState.MeleeAttack, ECharacterAttackState.PostMeleeAttack),

                new FSMTransition_CharacterAttack(ECharacterAttackState.PostMeleeAttack, ECharacterAttackState.Idle),
            };
        }
    }
}