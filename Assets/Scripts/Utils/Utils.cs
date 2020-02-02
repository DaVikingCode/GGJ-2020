using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    static public Color GetColor(float hue, float lightness, float hueScale = 360f, float lightnessScale = 100f)
    {
        hue = hue / hueScale;
        lightness = lightness / lightnessScale;
        return Color.HSVToRGB(hue, 1f, lightness);
    }

}
