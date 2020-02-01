using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : BaseUIScreen
{
    public Image frame;
    public Image background;

    public void SetTarget(float hue, float lightness, float hueScale = 360f, float lightnessScale = 100f)
    {
        Color c = GetColor(hue, lightness, hueScale, lightness);
        frame.color = c;
    }

    public void SetCurrent(float hue, float lightness, float hueScale = 360f, float lightnessScale = 100f)
    {
        Color c = GetColor(hue, lightness, hueScale, lightness);
        background.color = c;
    }


    public Color GetColor(float hue, float lightness, float hueScale = 360f, float lightnessScale = 100f)
    {
        hue = hue / hueScale;
        lightness = lightness / lightnessScale;
        return Color.HSVToRGB(hue, 1f, lightness);
    }



}
