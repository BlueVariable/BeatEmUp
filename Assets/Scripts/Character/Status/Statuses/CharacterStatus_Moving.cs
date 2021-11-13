using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_Moving : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterMovement _movementFSM = null;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.Moving;
        }

        public override bool CheckStatus()
        {
            _movementFSM.TryGetCurStateType(out ECharacterMovementState curMovementState);

            return curMovementState == ECharacterMovementState.Move;
        }

        public override void AppearStatus()
        {
            
        }

        public override void DisappearStatus()
        {
            
        }
    }
}