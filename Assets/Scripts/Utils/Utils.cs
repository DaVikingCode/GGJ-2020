using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    public static float saturation = 0.5f;

    static public Color GetColor(float hue, float lightness, float hueScale = 360f, float lightnessScale = 100f)
    {
        hue = hue / hueScale;
        lightness = lightness / lightnessScale;

        return Color.HSVToRGB(hue, saturation, lightness);
    }

}
