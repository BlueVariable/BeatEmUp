using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    public class CharacterAnimationCallback
    {
        public Action Callback { get; }
        
        public float NormalizedTime { get; }

        public bool IsPersistent { get; } = false;

        public CharacterAnimationCallback(Action callback, float normalizedTime, bool isPersistent = false)
        {
            Callback = callback;
            NormalizedTime = Mathf.Clamp(
                normalizedTime,
                AnimationStateEventBehaviour.MIN_NORMALIZED_TIME,
                AnimationStateEventBehaviour.MAX_NORMALIZED_TIME);

            IsPersistent = isPersistent;
        }
    }

    public abstract class CustomStateMachineBehaviour : StateMachineBehaviour
    {
        [SerializeField] private string _fullPath = string.Empty;

        public int PathHash => Animator.StringToHash(_fullPath);
    }

    public class AnimationStateEventBehaviour : CustomStateMachineBehaviour
    {
        #region Constants

        public const float MIN_NORMALIZED_TIME = 0.05f;
        public const float MAX_NORMALIZED_TIME = 0.95f;

        #endregion
        
        private Dictionary<CharacterAnimationCallback, bool> _callbackLookUp =
            new Dictionary<CharacterAnimationCallback, bool>();
        
        public Action OnStateEnterEvent { get; set; }
        
        public Action OnStateExitEvent { get; set; }
        
        public void AddCallback(CharacterAnimationCallback animCallback)
        {
            _callbackLookUp.Add(animCallback, false);
        }

        public void RemoveCallback(CharacterAnimationCallback animCallback)
        {
            if (_callbackLookUp.ContainsKey(animCallback))
            {
                _callbackLookUp.Remove(animCallback);
            }
        }
        
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnStateEnterEvent?.Invoke();
            
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float loopCount = (int) stateInfo.normalizedTime;

            float time = Mathf.Repeat(stateInfo.normalizedTime, 1.0f);

            if (time < MIN_NORMALIZED_TIME)
            {
                ResetCallbacks();
                
                base.OnStateUpdate(animator, stateInfo, layerIndex);
                
                return;
            }

            List<CharacterAnimationCallback> callbacksToDelete = new List<CharacterAnimationCallback>();

            List<CharacterAnimationCallback> keys = new List<CharacterAnimationCallback>(_callbackLookUp.Keys);
            
            foreach (CharacterAnimationCallback key in keys)
            {
                if (key.NormalizedTime < time && !_callbackLookUp[key])
                {
                    key.Callback?.Invoke();

                    _callbackLookUp[key] = true;

                    if (!key.IsPersistent)
                    {
                        callbacksToDelete.Add(key);
                    }
                }
            }

            foreach (CharacterAnimationCallback callback in callbacksToDelete)
            {
                _callbackLookUp.Remove(callback);
            }
            
            base.OnStateUpdate(animator, stateInfo, layerIndex);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnStateExitEvent?.Invoke();
        }

        private void ResetCallbacks()
        {
            List<CharacterAnimationCallback> keys = new List<CharacterAnimationCallback>(_callbackLookUp.Keys);
            
            foreach (CharacterAnimationCallback key in keys)
            {
                _callbackLookUp[key] = false;
            }
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}
    }
}