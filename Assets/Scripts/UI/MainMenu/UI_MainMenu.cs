using System;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class UI_MainMenu : UIBase
    {
        [SerializeField] private ButtonBase[] _buttons = null;
        
        protected override void ActivatingCustomActions(Action onCompleted = null)
        {
            Array.ForEach(_buttons, val => val.TryActivate());
            
            base.ActivatingCustomActions(onCompleted);
        }

        protected override void DeactivatingCustomActions(Action onCompleted = null)
        {
            Array.ForEach(_buttons, val => val.TryDeactivate());
            
            base.DeactivatingCustomActions(onCompleted);
        }
    }
}