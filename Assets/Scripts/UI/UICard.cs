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
    public Image mainSymbol;
    public Shadow shadow;

    public float shadowDist = 15f;

    protected RectTransform rect;

    private void Awake()
    {
        this.rect = this.gameObject.GetComponent<RectTransform>();
        ShowFront(false);

        shadowTarget = new Vector2(shadowDist, -shadowDist);
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
    }

    public void ShowFront(bool value = true)
    {

        front.gameObject.SetActive(value);
        back.gameObject.SetActive(!value);

    }

    public void PopCard()
    {
        StartCoroutine(CPopCard());
    }

    IEnumerator CPopCard()
    {
        Quaternion startRotation = Quaternion.AngleAxis(UnityEngine.Random.Range(-45f, 45f), Vector3.forward);
        Quaternion targetRotation = Quaternion.AngleAxis(UnityEngine.Random.Range(-10f, 10f), Vector3.forward);

        Vector2 targetPosition = this.rect.anchoredPosition;
        Vector2 startPosition = targetPosition + new Vector2(0f, -1000f);
        yield return GameManager.instance.animationManager.Animate(1f, (float t) =>
         {
             this.rect.localRotation = Quaternion.LerpUnclamped(startRotation, targetRotation, t);
            
             this.rect.anchoredPosition = Vector2.LerpUnclamped(startPosition, targetPosition, t);
             return true;
         }, AnimationManager.EASING.ELASTIC_IN,()=>
         {
             this.Flip();
         });
    }

    public void SwipeCard(bool right)
    {

    }

    public void Flip()
    {
        StartCoroutine(CFlip());
    }

    IEnumerator CFlip()
    {
        float dur = 0.25f;

        Quaternion rota = this.rect.localRotation * Quaternion.AngleAxis(-90f, Vector3.up);
        Quaternion rotb = this.rect.localRotation * Quaternion.AngleAxis(0f, Vector3.up) * this.rect.localRotation;
        Quaternion rotc = this.rect.localRotation * Quaternion.AngleAxis(90f, Vector3.up) * this.rect.localRotation;

        rect.rotation = rota;

        for (float t  = 0f; t < dur; t += Time.unscaledDeltaTime)
        {
            float t1 =  (dur - t) / dur;
            rect.rotation = Quaternion.Slerp(rota, rotb, t1);   
            yield return null;
        }

        rect.rotation = rotb;

        ShowFront(!front.gameObject.activeInHierarchy);

        for (float t = 0f; t < dur; t += Time.unscaledDeltaTime)
        {
            float t1 =  (dur - t) / dur;
            rect.rotation = Quaternion.Slerp(rotb, rotc, t1);
            yield return null;
        }

        rect.rotation = rotb;
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