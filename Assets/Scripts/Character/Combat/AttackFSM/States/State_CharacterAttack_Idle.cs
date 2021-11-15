using BLUE.FSMSystem;
using BLUE.ProjectX.Character;
using UnityEngine;

namespace BLUE.ProjectX.Character.States
{
    public class State_CharacterAttack_Idle : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.Idle;
        }
    }
}