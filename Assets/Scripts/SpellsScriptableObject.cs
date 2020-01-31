using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class SpellsScriptableObject : ScriptableObject
{
    public SpellData[] spells;

#if UNITY_EDITOR
    [MenuItem("Tools/Create spells")]
    static void CreateSpells()
    {
        SpellsScriptableObject data = CreateInstance<SpellsScriptableObject>();

        AssetDatabase.CreateAsset(data, "Assets/Scripts/Spells.asset");
        EditorUtility.SetDirty(data);
        AssetDatabase.SaveAssets();
    }
#endif
}

[Serializable]
public class SpellData
{
    public Sprite front;

    public int a;
    public int b;
    public int c;
}
