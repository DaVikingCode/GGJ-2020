﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : BaseUIScreen
{
    public Image frame;
    public Image background;
    public UICard card;
	public Slider slider;


    [HideInInspector]
    public bool animatingColors = false;

    public void SetTarget(float hue, float lightness)
    {
        Color c = Utils.GetColor(hue % 360f, lightness % 100f, 360f, 100f);
        frame.color = c;
    }

    public void SetCurrent(float hue, float lightness)
    {
        UnclampedHSV to = new UnclampedHSV()
        {
            hue = hue/360f,
            val = lightness/100f
        };
        UnclampedHSV from = UnclampedHSV.fromColor(background.color);

        this.animatingColors = true;
        AnimateColorInHSLSpace(background, from, to, 0.5f);
    }

	public void SetCurrent2(Spell currentSpell, CardData currentCard) {
		UnclampedHSV to = new UnclampedHSV()
		{
			hue = (currentSpell.hue %360f) / 360f,
			val = currentSpell.lightness/100f
		};



        if(currentCard.type == CARD_TYPE.HUE)
        {
            if (currentCard.value == 360)
                to.hue += 1f;
            else
                to.hue += currentCard.value/360f;
        }else
        {
            to.val += currentCard.value/100f;
        }


		UnclampedHSV from = UnclampedHSV.fromColor(background.color);

		this.animatingColors = true;
		AnimateColorInHSLSpace(background, from, to, 0.5f);
	}

    public Coroutine AnimateColorInHSLSpace(Image image, UnclampedHSV from, UnclampedHSV to, float duration)
    {


        return GameManager.instance.animationManager.Animate(duration, (float t) =>
         {
             float h = 0f;
             float l = 0f;

             h = Mathf.LerpUnclamped(from.hue, to.hue, t);
             //s = Mathf.LerpUnclamped(sA, sB, t);
             l = Mathf.LerpUnclamped(from.val, to.val, t);

             image.color = Color.HSVToRGB(Mathf.Repeat(h,1f), Utils.saturation, Mathf.Clamp01(l));
             return true;

         }, AnimationManager.EASING.LINEAR,()=>
         {
             this.animatingColors = false;
         });
    }

}


public class UnclampedHSV
{
    public float hue;
    public float sat;
    public float val;
    public Color ToColor()
    {
        return Color.HSVToRGB(hue, sat, val);
    }

    public static UnclampedHSV fromColor(Color c)
    {
        UnclampedHSV res = new UnclampedHSV();
        Color.RGBToHSV(c, out res.hue, out res.sat, out res.val);
        return res;
    }
}