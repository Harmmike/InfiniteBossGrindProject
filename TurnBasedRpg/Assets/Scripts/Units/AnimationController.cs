using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator unitAnimator;
    private List<string> animationStateBools = new List<string>()
    {
        "IsIdle",
        "IsAttacking",
        "TakingDamage",
        "IsDead"
    };

    public AnimationClip testIdle;

    public IEnumerator PlayAnimation(int animationKey)
    {
        foreach (var state in animationStateBools)
        {
            unitAnimator.SetBool(state, false);
        }

        unitAnimator.SetBool(animationStateBools[animationKey], true);

        yield return new WaitForSeconds(1.15f);

        ReturnToIdle();
    }

    public void ReturnToIdle()
    {
        foreach (var state in animationStateBools)
        {
            unitAnimator.SetBool(state, false);
        }

        unitAnimator.SetBool("IsIdle", true);
    }
}
