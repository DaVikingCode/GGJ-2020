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
}
