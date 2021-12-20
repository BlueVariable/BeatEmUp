using BLUE.PoolingSystem;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class FloatingTextData
    {
        public string Text { get; }
        
        public bool OverrideColor { get; }
        
        public Color TextColor { get; }

        public FloatingTextData(string text, bool overrideColor = false, Color textColor = default)
        {
            Text = text;
            OverrideColor = overrideColor;
            TextColor = textColor;
        }
    }
    
    public class UI_FloatingTextView : UIBase
    {
        [SerializeField] private Pool_MB _floatingTextPool = null;
        
        public void ActivateFloatingText(FloatingTextData data)
        {
            PoolObject poolObject = _floatingTextPool.AllocatePoolObject();

            UI_FloatingText floatingText = poolObject.GetComponent<UI_FloatingText>();

            floatingText.ActivateFloatingText(data, transform.position, OnFloatingTextSequenceCompleted);
        } 

        private void OnFloatingTextSequenceCompleted(UI_FloatingText floatingText)
        {
            _floatingTextPool.DeallocatePoolObject(floatingText.GetComponent<PoolObject>());
        }
    }
}