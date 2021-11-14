using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public abstract class CharacterStatusBase : MonoBehaviour
    {
        [SerializeField] protected bool _breakIfStatusAppear = false;

        public bool BreakIfStatusAppear => _breakIfStatusAppear;
        
        public bool IsAppearing { get; private set; }

        public abstract ECharacterStatus GetCharacterStatus();
        
        public abstract bool CheckStatus();

        public void AppearStatus()
        {
            IsAppearing = true;
            
            AppearStatusCustomActions();
        }

        public void DisappearStatus()
        {
            IsAppearing = false;
            
            DisappearedStatusCustomActions();
        }

        protected virtual void AppearStatusCustomActions() { }
        
        protected virtual void DisappearedStatusCustomActions() { }
    }
}