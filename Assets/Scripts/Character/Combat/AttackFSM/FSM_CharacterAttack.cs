using System;
using BLUE.FSMSystem;
using System.Collections.Generic;

namespace BLUE.ProjectX.Character
{
    public enum ECharacterAttackState
    {
        None = 0,
        Idle = 1,
        
        Anticipation = 2,
        Recovery = 3,
        
        MeleeAttack = 21,
        
        RangedAttack = 31,
        
        Death = 40,
    }

    [Serializable]
    public class FSMTransitionMessage_CharacterAttack : FSMTransitionMessageBase
    {
        public EAttackType AttackType { get; }
        
        public CharacterAttackSettings AttackSettings { get; }

        public FSMTransitionMessage_CharacterAttack(EAttackType attackType, CharacterAttackSettings attackSettings)
        {
            AttackType = attackType;
            AttackSettings = attackSettings;
        }
    }
    
    public class FSM_CharacterAttack : FSMBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack, FSMTransition_CharacterAttack>
    {
        protected override List<FSMTransition_CharacterAttack> GetInitialPossibleTransitions()
        {
            return new List<FSMTransition_CharacterAttack>()
            {
                new FSMTransition_CharacterAttack(ECharacterAttackState.Idle, ECharacterAttackState.Anticipation),

                new FSMTransition_CharacterAttack(ECharacterAttackState.Anticipation, ECharacterAttackState.MeleeAttack),
                new FSMTransition_CharacterAttack(ECharacterAttackState.Anticipation, ECharacterAttackState.RangedAttack),
                new FSMTransition_CharacterAttack(ECharacterAttackState.Anticipation, ECharacterAttackState.Idle),

                new FSMTransition_CharacterAttack(ECharacterAttackState.MeleeAttack, ECharacterAttackState.Recovery),
                new FSMTransition_CharacterAttack(ECharacterAttackState.MeleeAttack, ECharacterAttackState.Idle),

                new FSMTransition_CharacterAttack(ECharacterAttackState.RangedAttack, ECharacterAttackState.Recovery),
                new FSMTransition_CharacterAttack(ECharacterAttackState.RangedAttack, ECharacterAttackState.Idle),

                new FSMTransition_CharacterAttack(ECharacterAttackState.Recovery, ECharacterAttackState.Idle),
            };
        }
    }
}