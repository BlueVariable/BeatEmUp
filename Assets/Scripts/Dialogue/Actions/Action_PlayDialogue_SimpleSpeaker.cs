using System;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ProjectX.Dialogue
{
    public class Action_PlayDialogue_SimpleSpeaker : InterruptableActionBase
    {
        [SerializeField] private ESimpleSpeakerDialogue[] _dialogueOptions = new ESimpleSpeakerDialogue[0];

        [SerializeField] [Range(0, 1)] private float _dialoguePlayProbability = 1f;
        
        [SerializeField] private bool _autoStopOnComplete = true;

        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion
        
        public override void Execute()
        {
            if (!CanPlay())
            {
                OnCompleted?.Invoke(this);

                return;
            }
            
            ESimpleSpeakerDialogue dialogueType = _dialogueOptions[UnityEngine.Random.Range(0, _dialogueOptions.Length)];
            
            DialoguePlayer_SimpleSpeaker.Instance.Play(
                dialogueType,
                () => OnCompleted?.Invoke(this),
                _autoStopOnComplete);
        }

        public override void Interrupt()
        {
            DialoguePlayer_SimpleSpeaker.Instance.Stop();
        }

        private bool CanPlay()
        {
            return _dialoguePlayProbability > UnityEngine.Random.Range(0, 1);
        }
    }
}