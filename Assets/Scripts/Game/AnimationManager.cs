using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public enum EASING
    {
        LINEAR,
        ELASTIC_IN,
        ELASTIC_OUT,
        EASE_IN,
        EASE_OUT,
        EASE_INOUT
    }

    public AnimationCurve linear;
    public AnimationCurve elasticIn;
    public AnimationCurve elasticOut;
    public AnimationCurve easeIn;
    public AnimationCurve easeInOut;
    public AnimationCurve easeOut;

    public Coroutine Animate(float duration, System.Func<float,bool> update, EASING easing, System.Action onComplete = null)
    {
        return StartCoroutine(AnimateCoroutine(duration, update, easing, onComplete));
    }

    IEnumerator AnimateCoroutine(float duration, System.Func<float,bool> update, EASING easing, System.Action onComplete = null)
    {
        AnimationCurve curve = this.linear;
        switch(easing)
        {
            case EASING.ELASTIC_IN:
                curve = this.elasticIn;
                break;
            case EASING.ELASTIC_OUT:
                curve = this.elasticOut;
                break;
            case EASING.EASE_IN:
                curve = this.easeIn;
                break;
            case EASING.EASE_OUT:
                curve = this.easeOut;
                break;
            case EASING.EASE_INOUT:
                curve = this.easeInOut;
                break;
        }

        update(0f);

        for(float time = 0f; time < duration; time += Time.deltaTime)
        {
            float t = (duration - time) / duration;
            float t1 = curve.Evaluate(t);
            if(update.Invoke(t1))
            {

            }
            else
            {
                yield break;
            }
            yield return null;
        }


    }
}
