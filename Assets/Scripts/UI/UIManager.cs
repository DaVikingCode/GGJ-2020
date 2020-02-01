using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public List<BaseUIScreen> screens;

    private void Awake()
    {
        instance = this;

        screens = new List<BaseUIScreen>();
        BaseUIScreen[] childScreens = this.gameObject.GetComponentsInChildren<BaseUIScreen>();
        foreach(BaseUIScreen c in childScreens)
        {
            screens.Add(c);
        }

    }

    private void OnDestroy()
    {
        instance = null;
    }
}
