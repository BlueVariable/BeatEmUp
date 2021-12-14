using BLUE.DamageSystem;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterAttack_Death : StateBase<ECharacterAttackState, FSMTransitionMessage_CharacterAttack>
    {
        [SerializeField] private Damagable _damagable = null;

        [SerializeField] private FSM_CharacterAttack _attackFSM = null;
        
        public override ECharacterAttackState GetStateType()
        {
            return ECharacterAttackState.Death;
        }
        
        #region DamagableRelated
        
        private void Awake()
        {
            RegisterToDamagable();
        }

        private void OnDestroy()
        {
            UnregisterFromDamagable();
        }

        private void RegisterToDamagable()
        {
            _damagable.OnDied += OnDied;
        }

        private void UnregisterFromDamagable()
        {
            _damagable.OnDied -= OnDied;
        }

        private void OnDied()
        {
            _attackFSM.TryChangeState(ECharacterAttackState.Death);
        }

        #endregion
    }
}