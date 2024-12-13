using UnityEngine;

namespace ValeryPopov.Common.Animation
{
    public static class AnimationReplacer
    {
        public static void ReplaceAnimation(Animator animator, string animationName, AnimationClip clip)
        {
            AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = aoc;
            aoc[animationName] = clip;
        }
    }
}