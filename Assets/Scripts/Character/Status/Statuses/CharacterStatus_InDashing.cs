using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_InDashing : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterMovement _characterMovementFSM = null;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.Dashing;
        }

        public override bool CheckStatus()
        {
            _characterMovementFSM.TryGetCurStateType(out ECharacterMovementState curMovementState);

            return curMovementState == ECharacterMovementState.Dash;
        }

        public override void AppearStatus()
        {
            // play dash animation
        }

        public override void DisappearStatus()
        {
            
        }
    }
}