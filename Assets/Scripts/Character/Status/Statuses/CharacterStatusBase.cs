using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public abstract class CharacterStatusBase : MonoBehaviour
    {
        [SerializeField] protected bool _breakIfStatusAppears = false;
        public bool BreakIfStatusAppears => _breakIfStatusAppears;
        
        public bool IsActive { get; private set; }

        public abstract ECharacterStatus GetCharacterStatus();
        
        public abstract bool CheckStatus();

        protected virtual void StatusAppearedCustomActions() { }
        
        protected virtual void StatusDisappearedCustomActions() { }
        
        public void StatusAppeared()
        {
            IsActive = true;
            
            StatusAppearedCustomActions();
        }

        public void StatusDisappeared()
        {
            IsActive = false;
            
            StatusDisappearedCustomActions();
        }
    }
}