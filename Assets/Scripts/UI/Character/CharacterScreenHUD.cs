using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class CharacterScreenHUD : UIBase
    {
        protected override void AwakeCustomActions()
        {
            RegisterToEvents();

            base.AwakeCustomActions();
        }

        protected override void DestroyCustomActions()
        {
            UnregisterFromEvents();
            
            base.DestroyCustomActions();
        }

        private void RegisterToEvents()
        {
            EventRaiser_OnCharacterScreenHUDToggleRequested.OnRaisedStatic += OnToggleRequested;
        }

        private void UnregisterFromEvents()
        {
            EventRaiser_OnCharacterScreenHUDToggleRequested.OnRaisedStatic -= OnToggleRequested;
        }

        private void OnToggleRequested(object sender, EventArgs_OnCharacterScreenHUDToggleRequested e)
        {
            if (e.IsActive)
            {
                TryActivate();
            }
            else
            {
                TryDeactivate();
            }
        }
    }
}