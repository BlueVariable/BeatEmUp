using System.Linq;
using UnityEngine;
using BLUE.ProjectX.Character;

namespace Extensions
{
    public static class AnimatorExtensions
    {
        public static T TryGetBehaviour<T>(this Animator animator, string fullPath) where T : CustomStateMachineBehaviour
        {
            T behaviour = animator.GetBehaviours<T>().ToList()
                .First(i => i.PathHash == Animator.StringToHash(fullPath));

            return behaviour;
        }
    }
}