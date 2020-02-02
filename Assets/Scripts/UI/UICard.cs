using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIExtensions;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UICard : MonoBehaviour
{
    public Image front;
    public Image back;
    public Image tlSymbol;
    public Image brSymbol;
    public Image mainSymbol;
    public Shadow shadow;

    public float shadowDist = 15f;

    protected RectTransform rect;

    [HideInInspector]
    public bool isAnimating = false;

    CanvasGroup group;

    private void Awake()
    {
        this.rect = this.gameObject.GetComponent<RectTransform>();
        ShowFront(false);

        shadowTarget = new Vector2(shadowDist, -shadowDist);

        group = GetComponent<CanvasGroup>();
        if (group == null)
            group = this.gameObject.AddComponent<CanvasGroup>();
    }

    Vector2 shadowTarget;

    private void OnValidate()
    {
        this.rect = this.gameObject.GetComponent<RectTransform>();
        shadowTarget = new Vector2(shadowDist, -shadowDist);
        UpdateShadow();
    }

    private void Update()
    {
        UpdateShadow();
    }

    void UpdateShadow()
    {
        Quaternion rot = this.rect.localRotation;
        Vector2 shadowAbs = Quaternion.Inverse(rot) * shadowTarget;
        shadow.effectDistance = shadowAbs;
    }

    public void SetSymbol(Sprite sprite)
    {
        mainSymbol.sprite = sprite;
        mainSymbol.SetNativeSize();

        tlSymbol.sprite = sprite;
        tlSymbol.SetNativeSize();

        brSymbol.sprite = sprite;
        brSymbol.SetNativeSize();
    }

    public void ShowFront(bool value = true)
    {

        front.gameObject.SetActive(value);
        back.gameObject.SetActive(!value);

    }

    public void SwipeLeft(System.Action onComplete = null)
    {
        this.isAnimating = true;
        StartCoroutine(CSwipe(false,onComplete));
    }

    public void SwipeRight(System.Action onComplete = null)
    {
        this.isAnimating = true;
        StartCoroutine(CSwipe(true,onComplete));
    }

    IEnumerator CSwipe(bool swipeRight,System.Action onComplete = null)
    {
        float startAlpha = 1f;
        float endAlpha = 0f;

        Quaternion startRotation = this.rect.localRotation;
        Quaternion targetRotation = Quaternion.AngleAxis(UnityEngine.Random.Range(-45f, 45f), Vector3.forward);

        Vector2 startPosition = this.rect.anchoredPosition;
        Vector2 targetPosition = startPosition + new Vector2(1000f * (swipeRight ? 1f : -1f), 0f);

        yield return GameManager.instance.animationManager.Animate(0.25f, (float t) =>
        {
            this.rect.localRotation = Quaternion.LerpUnclamped(startRotation, targetRotation, t);
            this.rect.anchoredPosition = Vector2.LerpUnclamped(startPosition, targetPosition, t);
            this.group.alpha = Mathf.LerpUnclamped(startAlpha, endAlpha, t);

            return true;

        }, AnimationManager.EASING.EASE_IN, () =>
        {
            this.group.alpha = startAlpha;
            onComplete?.Invoke();
            this.isAnimating = false;
        });
        
    }

    public void PopCard(System.Action onComplete = null)
    {
        StartCoroutine(CPopCard(onComplete));
    }

    IEnumerator CPopCard(System.Action onComplete = null)
    {
        this.isAnimating = true;

        ShowFront(false);

        Quaternion startRotation = Quaternion.AngleAxis(UnityEngine.Random.Range(-45f, 45f), Vector3.forward);
        Quaternion targetRotation = Quaternion.AngleAxis(UnityEngine.Random.Range(-10f, 10f), Vector3.forward);

        Vector2 targetPosition = Vector2.zero;
        Vector2 startPosition = targetPosition + new Vector2(0f, -1000f);

        yield return GameManager.instance.animationManager.Animate(0.25f, (float t) =>
         {
             this.rect.localRotation = Quaternion.LerpUnclamped(startRotation, targetRotation, t);
             this.rect.anchoredPosition = Vector2.LerpUnclamped(startPosition, targetPosition, t);
             return true;
         }, AnimationManager.EASING.EASE_OUT,()=>
         {
             this.isAnimating = false;
             onComplete?.Invoke();
             this.Flip();
         });


    }

    public void Flip()
    {
        this.isAnimating = true;
        StartCoroutine(CFlip());
    }

    IEnumerator CFlip()
    {
        float dur = 0.25f;

        Quaternion startRotation = this.rect.localRotation;

        Quaternion rota = startRotation * Quaternion.AngleAxis(0f, Vector3.up);
        Quaternion rotb = startRotation * Quaternion.AngleAxis(90f, Vector3.up);
        Quaternion rotc = startRotation * Quaternion.AngleAxis(0f, Vector3.up);

        Vector2 startScale = this.rect.localScale;
        Vector2 popScale = startScale * 1.05f;

        rect.rotation = rota;
        rect.localScale = startScale;

        for (float t  = 0f; t < dur; t += Time.unscaledDeltaTime)
        {
            float t1 =  t / dur;
            rect.rotation = Quaternion.Slerp(rota, rotb, t1);
            rect.localScale = Vector2.LerpUnclamped(startScale, popScale,t1);
            yield return null;
        }

        rect.rotation = rotb;

        ShowFront(!front.gameObject.activeInHierarchy);

        for (float t = 0f; t < dur; t += Time.unscaledDeltaTime)
        {
            float t1 =  t / dur;
            rect.rotation = Quaternion.Slerp(rotb, rotc, t1);
            rect.localScale = Vector2.LerpUnclamped(popScale, startScale, t1);
            yield return null;
        }

        rect.rotation = rota;
        rect.localScale = startScale;

        this.isAnimating = false;
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(UICard))]
public class UICardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UICard card = (UICard)target;
        if (card == null) return;

        if (GUILayout.Button("Flip"))
        {
            card.Flip();
        }
    }
}
#endif