using BLUE.UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace BLUE.ProjectX.UI
{
    public class UI_Button_Category : ButtonBase
    {
        [SerializeField] private Image _selectedItemIcon = null;

        public void SetSelectedItemIcon(Sprite sprite)
        {
            _selectedItemIcon.sprite = sprite;
        }
    }
}