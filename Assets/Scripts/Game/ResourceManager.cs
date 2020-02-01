using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	public int a = 0;
	public int b = 0;
	public int c = 0;

	public void resetValues()
	{
		a = 0;
		b = 0;
		c = 0;
	}

	public void addResource(char type, int value)
	{
		switch (type)
		{
			case 'a':
				a += value;
				break;
			case 'b':
				b += value;
				break;
			case 'c':
				c += value;
				break;
		}
	}
}
