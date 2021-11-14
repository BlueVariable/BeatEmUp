using BLUE.FSMSystem;
using BLUE.Helpers;
using UnityEngine;

namespace Character.Combat.AttackFSM.States
{
    public class State_CharacterAttack_PostMeleeAttack : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private FSM_CharacterAttack _attackFSM = null;

        [SerializeField] private float _recoveryDuration = 0.25f;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.PostMeleeAttack;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_CharacterAttack transitionMessage)
        {
            CoroutineHelper.Instance.WaitForSeconds(_recoveryDuration, () =>
            {
                _attackFSM.TryChangeState(ECharacterAttackState.Idle);
            });
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}