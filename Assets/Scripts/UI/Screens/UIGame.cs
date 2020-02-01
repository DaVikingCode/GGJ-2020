using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : BaseUIScreen
{
    public Image rA;
    public Image rB;
    public Image rC;

    public TextMeshProUGUI tA;
    public TextMeshProUGUI tB;
    public TextMeshProUGUI tC;

    public void SetResourceDisplay(int resourceIndex, int value)
    {
        switch(resourceIndex)
        {
            case 0:
                StartCoroutine(PopResource(rA));
                tA.text = value.ToString();
                break;
            case 1:
                StartCoroutine(PopResource(rB));
                tB.text = value.ToString();
                break;
            case 2:
                StartCoroutine(PopResource(rC));
                tC.text = value.ToString();
                break;
        }
    }

    IEnumerator PopResource(Image img)
    {
        Vector3 scale1 = img.rectTransform.localScale;
        Vector3 scaleB = new Vector3(1.4f, 1.4f, 1f);

        float dur = 1f;
        for(float time = 0f; time < dur; time += Time.deltaTime)
        {
            img.rectTransform.localScale = Vector3.LerpUnclamped(scale1, scaleB, Mathf.Cos(time * 0.02f));
            yield return null;
        }

        img.rectTransform.localScale = scale1;
    }

}
