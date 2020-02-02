using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiKeyboardKey : MonoBehaviour
{
    Image img;
    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void Pop()
    {
        StartCoroutine(CPop());
    }

    IEnumerator CPop()
    {
        yield return GameManager.instance.animationManager.Animate(0.5f,(float t )=> {

            return true;
        }, AnimationManager.EASING.EASE_IN);
        yield break;
    }
}
