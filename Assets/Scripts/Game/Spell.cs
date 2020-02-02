using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Spell
{
	public int hue;
	public int lightness;

	public Spell(int aHue, int aLightness) {
		this.hue = aHue;
		this.lightness = aLightness;
	}
    public void AddCardValues(CardData card)
    {
        switch(card.type)
        {
            case CARD_TYPE.HUE:
                this.hue += card.value;
                break;
            case CARD_TYPE.LIGHTNESS:
                this.lightness = Mathf.Clamp(this.lightness + card.value, 25, 75);
                break;
        }
    } 
}
