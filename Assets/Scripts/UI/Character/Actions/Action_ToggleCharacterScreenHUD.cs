using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class Action_ToggleCharacterScreenHUD : ActionBase
    {
        [SerializeField] private EventArgs_OnCharacterScreenHUDToggleRequested _eventArgs = 
            new EventArgs_OnCharacterScreenHUDToggleRequested();
        
        private EventRaiser_OnCharacterScreenHUDToggleRequested _toggleRequestor =
            new EventRaiser_OnCharacterScreenHUDToggleRequested();
        
        public override void Execute()
        {
            _toggleRequestor.Raise(_eventArgs);
        }
    }
}