using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [HideInInspector]
    public List<BaseUIScreen> screens;

    private void Awake()
    {
        instance = this;

        screens = new List<BaseUIScreen>();
        BaseUIScreen[] childScreens = this.gameObject.GetComponentsInChildren<BaseUIScreen>(true);
        foreach(BaseUIScreen c in childScreens)
        {
            c.gameObject.SetActive(false);
            screens.Add(c);
        }

    }

    public void SwitchScreen<T>() where T : BaseUIScreen
    {
        for(int i = 0; i < screens.Count; i++)
        {
            BaseUIScreen screen = screens[i];
            if(screen.GetType() == typeof(T))
            {
                screen.gameObject.SetActive(true);
            }
            else
            {
                screen.gameObject.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
