using BLUE.FSMSystem;
using BLUE.Helpers;
using UnityEngine;

namespace Character.Combat.AttackFSM.States
{
    public class State_CharacterAttack_MeleeAttack : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private FSM_CharacterAttack _attackFSM = null;
        
        [SerializeField] private float _attackDuration = 0.5f;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.MeleeAttack;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            CoroutineHelper.Instance.WaitForSeconds(_attackDuration, () =>
            {
                _attackFSM.TryChangeState(ECharacterAttackState.PostMeleeAttack);

            });
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}