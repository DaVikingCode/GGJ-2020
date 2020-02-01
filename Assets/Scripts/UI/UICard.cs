using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UICard : MonoBehaviour
{
    public Image front;
    public Image back;
    public Image mainSymbol;

    protected RectTransform rect;

    private void Awake()
    {
        this.rect = this.gameObject.GetComponent<RectTransform>();
        ShowFront();
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

    public void Flip()
    {
        StartCoroutine(CFlip());
    }

    IEnumerator CFlip()
    {
        float dur = 0.25f;

        Quaternion rota = Quaternion.AngleAxis(-90f, Vector3.up);
        Quaternion rotb = Quaternion.AngleAxis(0f, Vector3.up);
        Quaternion rotc = Quaternion.AngleAxis(90f, Vector3.up);

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