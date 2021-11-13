using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public abstract class CharacterStatusBase : MonoBehaviour
    {
        [SerializeField] protected bool _breakIfStatusAppear = false;

        public bool BreakIfStatusAppear => _breakIfStatusAppear;

        public abstract ECharacterStatus GetCharacterStatus();
        
        public abstract bool CheckStatus();

        public abstract void AppearStatus();

        public abstract void DisappearStatus();
    }
}