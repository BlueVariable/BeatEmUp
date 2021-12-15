using System;
using BLUE.UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace BLUE.ProjectX.UI
{
   public abstract class UI_InventoryItemBase : UIBase
   {
      [SerializeField] private ButtonBase _button = null;
      public ButtonBase Button => _button;
      
      [SerializeField] private UI_SelectableBase _selectable = null;
      public UI_SelectableBase Selectable => _selectable;

      public abstract void InitItem(Enum itemType);
      protected abstract InventoryItemUIData GetItemUIData(Enum itemType);

      protected virtual void InitItemCustomActions(Enum itemType)
      {
      }
      
      protected override void ActivatingCustomActions(Action onCompleted = null)
      {
         _button.TryActivate();

         _selectable.TryActivate();
         
         base.ActivatingCustomActions(onCompleted);
      }

      protected override void DeactivatingCustomActions(Action onCompleted = null)
      {
         _button.TryDeactivate();

         _selectable.TryDeactivate();
         
         base.DeactivatingCustomActions(onCompleted);
      }
   }
   
   public abstract class UI_InventoryItemBase<T> : UI_InventoryItemBase
      where T : Enum
   {
      [SerializeField] private Image _placeHolderImage = null;
      
      [SerializeField] private Image _targetImage = null;

      protected abstract InventoryItemUIData GetItemUIData(T itemType);

      protected virtual void InitItemCustomActions(T itemType)
      {
         
      }
      
      public override void InitItem(Enum itemType)
      {
         _placeHolderImage.enabled = false;

         _targetImage.sprite = GetItemUIData((T)itemType).ItemIcon;

         _targetImage.enabled = true;

         InitItemCustomActions((T)itemType);
      }

      protected sealed override void InitItemCustomActions(Enum itemType)
      {
         InitItemCustomActions((T)itemType);
         
         base.InitItemCustomActions(itemType);
      }

      protected sealed override InventoryItemUIData GetItemUIData(Enum itemType)
      {
         return GetItemUIData((T) itemType);
      }
   }
}