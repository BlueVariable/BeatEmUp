using System;
using BLUE.EventRaiserSystem;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    [Serializable]
    public class EventArgs_OnCharacterScreenHUDToggleRequested : EventArgs
    {
        [SerializeField] private bool _isActive = false;
        public bool IsActive => _isActive;

        public EventArgs_OnCharacterScreenHUDToggleRequested(bool isActive)
        {
            _isActive = isActive;
        }

        public EventArgs_OnCharacterScreenHUDToggleRequested()
        {
        }
    }

    public class EventRaiser_OnCharacterScreenHUDToggleRequested 
        : EventRaiserBase<EventArgs_OnCharacterScreenHUDToggleRequested>
    {
    }
}