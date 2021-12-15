using BLUE.DamageSystem;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterMovement_Death : StateBase<ECharacterMovementState, FSMTransitionMessage_CharacterMovement>
    {
        [SerializeField] private Damagable _damagable = null;

        [SerializeField] private FSM_CharacterMovement _movementFSM = null;
        
        public override ECharacterMovementState GetStateType()
        {
            return ECharacterMovementState.Death;
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
            _movementFSM.TryChangeState(ECharacterMovementState.Death);
        }

        #endregion
    }
}