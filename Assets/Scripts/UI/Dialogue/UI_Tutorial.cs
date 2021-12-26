using System;
using System.Collections.Generic;
using BLUE.ActionSystem;
using BLUE.DialogueSystem;
using BLUE.ProjectX.Dialogue;
using BLUE.TriggerSystem;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ProjectX.UI
{
    public class UI_Tutorial : UIBase
    {
        [Header("Dialogue Settings")]
        [SerializeField] private UI_Text _dialogueTextUI = null;

        [SerializeField] private TriggerBase _dialogueContinueTrigger = null;

        [Header("Actions")]
        [SerializeField] private ActionExecutor _dialogueDisplayingActions = null;
        [SerializeField] private ActionExecutor _dialogueDisplayedActions = null;
        
        public ESimpleSpeaker LastSpokenSpeaker { get; private set; }
        
        private Queue<string> _dialogueQueue;

        private Action _onCompletedCallback;
        
        protected override void AwakeCustomActions()
        {
            EventRaiser_OnDialogueActivationRequested<DialogueData_SimpleSpeaker<ESimpleSpeaker>>.OnRaisedStatic += OnNarrativeActivationRequested;
            EventRaiser_OnDialogueDeactivationRequested.OnRaisedStatic += OnNarrativeDeactivationRequested;
            
            base.AwakeCustomActions();
        }

        protected override void DestroyCustomActions()
        {
            EventRaiser_OnDialogueActivationRequested<DialogueData_SimpleSpeaker<ESimpleSpeaker>>.OnRaisedStatic -= OnNarrativeActivationRequested;
            EventRaiser_OnDialogueDeactivationRequested.OnRaisedStatic -= OnNarrativeDeactivationRequested;

            base.DestroyCustomActions();
        }

        protected override void ActivatedCustomActions()
        {
            DisplayNextInQueue();
            
            base.ActivatedCustomActions();
        }

        private void OnNarrativeActivationRequested(
            object sender, 
            EventArgs_OnDialogueActivationRequested<DialogueData_SimpleSpeaker<ESimpleSpeaker>> e)
        {
            if (e.DialogueData.Speaker != ESimpleSpeaker.Tutorial)
            {
                return;
            }
            
            LastSpokenSpeaker = e.DialogueData.Speaker;

            _dialogueQueue = new Queue<string>(e.DialogueData.SpeakerText);

            _onCompletedCallback = e.OnCompletedCallback;

            if (Status != EStatus.Activated)
            {
                TryActivate();
            }
            else
            {
                DisplayNextInQueue();
            }
        }

        private void OnNarrativeDeactivationRequested(object sender, EventArgs_OnDialogueDeactivationRequested e)
        {
            TryDeactivate();
        }
        
        private void DisplayNextInQueue()
        {
            void onTriggered(TriggerBase triggerBase)
            {
                _dialogueContinueTrigger.OnTriggered -= onTriggered;

                DisplayNextInQueue();
            }
            
            void onTextDisplayed()
            {
                _dialogueTextUI.OnTextDisplayed -= onTextDisplayed;

                _dialogueContinueTrigger.OnTriggered += onTriggered;
                
                _dialogueDisplayedActions.Execute();
            }

            if (_dialogueQueue.Count == 0)
            {
                _onCompletedCallback?.Invoke();

                return;
            }
            
            _dialogueTextUI.OnTextDisplayed += onTextDisplayed;
            
            _dialogueTextUI.Text = _dialogueQueue.Dequeue();
            
            _dialogueDisplayingActions.Execute();
        }
    }
}