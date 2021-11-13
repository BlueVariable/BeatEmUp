using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterStatus_InAir : CharacterStatusBase
    {
        [SerializeField] private FSM_CharacterPlatform _platformFSM = null;

        private bool _isCharacterInAir = false;
        
        public override ECharacterStatus GetCharacterStatus()
        {
            return ECharacterStatus.InAir;
        }

        public override bool CheckStatus()
        {
            return false;
        }

        public override void AppearStatus()
        {
            // play jump anim
        }

        public override void DisappearStatus()
        {
            // do nothing for now
        }

        private void Awake()
        {
            _platformFSM.OnStateEntered += OnStateEntered;
            _platformFSM.OnStateExited += OnStateExited;
        }

        private void OnDestroy()
        {
            _platformFSM.OnStateEntered -= OnStateEntered;
            _platformFSM.OnStateExited -= OnStateExited;
        }

        private void OnStateEntered(ECharacterPlatformState characterPlatformState)
        {
            if (characterPlatformState == ECharacterPlatformState.Jump ||
                characterPlatformState == ECharacterPlatformState.DoubleJump)
            {
                _isCharacterInAir = true;
            }
        }
        
        private void OnStateExited(ECharacterPlatformState characterPlatformState)
        {
            if (characterPlatformState == ECharacterPlatformState.Jump ||
                characterPlatformState == ECharacterPlatformState.DoubleJump)
            {
                _isCharacterInAir = false;
            }
        }
    }
}