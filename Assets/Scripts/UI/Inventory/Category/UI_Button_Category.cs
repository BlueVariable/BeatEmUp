using System;
using BLUE.UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace BLUE.ProjectX.UI
{
    public class UI_Button_Category : ButtonBase
    {
        [SerializeField] private Image _selectedItemIcon = null;

        [SerializeField] private UI_SelectableBase _selectable = null;

        protected override void ActivatingCustomActions(Action onCompleted = null)
        {
            _selectable.TryActivate();
            
            base.ActivatingCustomActions(onCompleted);
        }

        protected override void DeactivatingCustomActions(Action onCompleted = null)
        {
            _selectable.TryDeactivate();
            
            base.DeactivatingCustomActions(onCompleted);
        }

        public void SetSelectedItemIcon(Sprite sprite)
        {
            _selectedItemIcon.sprite = sprite;
        }
    }
}