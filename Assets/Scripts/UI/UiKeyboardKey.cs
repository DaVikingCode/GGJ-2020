using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiKeyboardKey : MonoBehaviour
{
    Image img;
	RectTransform rectTransform;

    private void Awake()
    {
        img = GetComponent<Image>();
		rectTransform = GetComponent<RectTransform>();
    }

    public void Pop(bool direction)
    {
        StartCoroutine(CPop(direction));
    }

    IEnumerator CPop(bool direction)
    {
		Vector2 scaleStart = rectTransform.localScale;
		Vector2 scaleEnd = direction ? new Vector2(1.3f, 1.3f) : new Vector2(1f,1f);

        yield return GameManager.instance.animationManager.Animate(0.5f,(float t )=> {

			this.rectTransform.localScale = Vector2.LerpUnclamped(scaleStart, scaleEnd, t);
            return true;
        }, AnimationManager.EASING.EASE_IN);
        yield break;
    }
}
