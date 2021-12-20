using System;
using BLUE.Helpers;
using BLUE.UISystem;
using TMPro;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class UI_FloatingText : UIBase
    {
        [SerializeField] private TextMeshProUGUI _text = null;

        [SerializeField] private float _hoverDuration = 0.25f;

        private Action<UI_FloatingText> _completedCallback;

        private Transform _defaultParent = null;
        
        public void ActivateFloatingText(FloatingTextData data, Vector3 initialPos, Action<UI_FloatingText> onCompletedCallback)
        {
            _completedCallback = onCompletedCallback;

            _defaultParent = transform.parent;
            
            transform.parent = UI_FloatingTextCarrier.Instance.transform;
            
            transform.position = initialPos;
            
            _text.SetText(data.Text);

            if (data.OverrideColor)
            {
                _text.color = data.TextColor;
            }

            CoroutineHelper.Instance.WaitForSeconds(0.1f, () =>
            {
                TryActivate();
            });
        }

        protected override void ActivatedCustomActions()
        {
            CoroutineHelper.Instance.WaitForSeconds(_hoverDuration, () => TryDeactivate());
            
            base.ActivatedCustomActions();
        }

        protected override void DeactivatedCustomActions()
        {
            transform.parent = _defaultParent;
            
            base.DeactivatedCustomActions();
        }
    }
}