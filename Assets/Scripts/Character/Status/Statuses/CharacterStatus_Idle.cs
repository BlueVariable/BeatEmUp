using Character.Combat.AttackFSM;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_Idle : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterMovement _movementFSM = null;

        [SerializeField] private FSM_CharacterPlatform _platformFSM = null;

        [SerializeField] private FSM_CharacterAttack _attackFSM = null;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.Idle;
        }

        public override bool CheckStatus()
        {
            _movementFSM.TryGetCurStateType(out ECharacterMovementState curMovementState);
            _platformFSM.TryGetCurStateType(out ECharacterPlatformState curPlatformState);
            _attackFSM.TryGetCurStateType(out ECharacterAttackState curAttackState);

            return curMovementState == ECharacterMovementState.Idle &&
                   curPlatformState == ECharacterPlatformState.Idle &&
                   curAttackState == ECharacterAttackState.Idle;
        }

        public override void AppearStatus()
        {
            // play idle anim
        }

        public override void DisappearStatus()
        {
            
        }
    }
}