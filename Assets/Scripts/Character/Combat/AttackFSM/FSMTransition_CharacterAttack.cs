using BLUE.FSMSystem;

namespace BLUE.ProjectX.Character
{
    public class FSMTransition_CharacterAttack : FSMTransitionBase<ECharacterAttackState>
    {
        public FSMTransition_CharacterAttack(ECharacterAttackState initialStateType, ECharacterAttackState targetStateType) 
            : base(initialStateType, targetStateType)
        {
        }
    }
}