using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : BaseUIScreen
{
    public Image frame;
    public Image background;
    public UICard card;

    [HideInInspector]
    public bool animatingColors = false;

    public void SetTarget(float hue, float lightness, float hueScale = 360f, float lightnessScale = 100f)
    {
        Color c = GetColor(hue, lightness, hueScale, lightness);
        frame.color = c;
    }

    public void SetCurrent(float hue, float lightness, float hueScale = 360f, float lightnessScale = 100f)
    {
        Color to = GetColor(hue, lightness, hueScale, lightness);
        Color from = background.color;

        this.animatingColors = true;
        AnimateColorInHSLSpace(background, from, to, 0.5f);
    }

    public Coroutine AnimateColorInHSLSpace(Image image, Color from, Color to, float duration)
    {
        float hA;
        float sA;
        float lA;
        Color.RGBToHSV(from, out hA, out sA, out lA);

        float hB;
        float sB;
        float lB;
        Color.RGBToHSV(to, out hB, out sB, out lB);

        return GameManager.instance.animationManager.Animate(duration, (float t) =>
         {
             float h = 0f;
             float s = 0.5f;
             float l = 0f;

             if(hA == hB)
             {
                 hB = hA + 1f;
             }

             h = Mathf.LerpUnclamped(hA, hB, t);
             //s = Mathf.LerpUnclamped(sA, sB, t);
             l = Mathf.LerpUnclamped(lA, lB, t);

             image.color = Color.HSVToRGB(Mathf.Repeat(h,1f), s, Mathf.Clamp01(l));
             return true;

         }, AnimationManager.EASING.LINEAR,()=>
         {
             this.animatingColors = false;
         });
    }


    public Color GetColor(float hue, float lightness, float hueScale = 360f, float lightnessScale = 100f)
    {
        hue = hue / hueScale;
        lightness = lightness / lightnessScale;
        return Color.HSVToRGB(hue, 1f, lightness);
    }



}
