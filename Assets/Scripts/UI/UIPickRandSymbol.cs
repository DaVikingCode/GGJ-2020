using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPickRandSymbol : MonoBehaviour
{
    public List<Sprite> sprites;
    public Image symbolSprite;
    
    IEnumerator Start()
    {
        do
        {
            Sprite s = sprites[(int)Random.Range(0f, sprites.Count)];
            symbolSprite.sprite = s;
            symbolSprite.SetNativeSize();
            symbolSprite.GetComponent<RectTransform>().localScale = Vector2.one * 0.8f;
            float waitTime = Random.Range(0.1f, 1f);
            yield return new WaitForSeconds(waitTime);
        } while (true);
    }
    
}
