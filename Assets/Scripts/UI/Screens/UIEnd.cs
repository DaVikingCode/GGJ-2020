using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnd : BaseUIScreen
{

    public RectTransform colors;
    public CanvasGroup colorsGroup;

    public Image current;
    public Image target;

    public Button tryAgainButton;

    void OnEnable()
    {
        colorsGroup.alpha = 0f;
        colors.localScale = Vector2.one;
    }

    public void ShowColors(System.Action onComplete)
    {
        StartCoroutine(CShow(onComplete));
    }

    IEnumerator CShow(System.Action onComplete)
    {
        Vector2 startScale = Vector2.one * 0.1f;
        Vector2 endScale = Vector2.one * 2f;

        yield return GameManager.instance.animationManager.Animate(0.25f, (float t) =>
        {
            colorsGroup.alpha = Mathf.Lerp(0f, 1f, t);
            colors.localScale = Vector2.Lerp(startScale, endScale, t);
            return true;
        }, AnimationManager.EASING.EASE_IN, onComplete);
    }

    void OnDisable()
    {
        colorsGroup.alpha = 0f;
    }
}
