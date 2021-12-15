using BLUE.DamageSystem;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class State_CharacterPlatform_Death : StateBase<ECharacterPlatformState, FSMTransitionMessage_CharacterPlatform>
    {
        [SerializeField] private Damagable _damagable = null;

        [SerializeField] private FSM_CharacterPlatform _platformFSM = null;
        
        public override ECharacterPlatformState GetStateType()
        {
            return ECharacterPlatformState.Death;
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
            _platformFSM.TryChangeState(ECharacterPlatformState.Death);
        }

        #endregion
    }
}