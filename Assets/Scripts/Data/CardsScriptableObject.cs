using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CardsScriptableObject : ScriptableObject
{
    public CardData[] cards;
	public int hueStep = 15;
	public int lightnessStep = 25;

#if UNITY_EDITOR
	[MenuItem("Tools/Create cards")]
    static void CreateCards()
    {
        CardsScriptableObject data = CreateInstance<CardsScriptableObject>();

        AssetDatabase.CreateAsset(data, "Assets/Scripts/Cards.asset");
        EditorUtility.SetDirty(data);
        AssetDatabase.SaveAssets();
    }
#endif
}

public enum CARD_TYPE
{
	UNDEFINED,
	HUE,
	LIGHTNESS
}

[Serializable]
public class CardData
{
    public Sprite front;

    public int value;
    public CARD_TYPE type;
}
