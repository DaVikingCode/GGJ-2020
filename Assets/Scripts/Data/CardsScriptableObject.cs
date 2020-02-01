using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CardsScriptableObject : ScriptableObject
{
    public CardData[] cards;

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

[Serializable]
public class CardData
{
    public Sprite front;

    public int a;
    public int b;
    public int c;
}
