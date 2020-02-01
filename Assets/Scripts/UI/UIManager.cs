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

    public T SwitchScreen<T>() where T : BaseUIScreen
    {
        BaseUIScreen res = null;

        for(int i = 0; i < screens.Count; i++)
        {
            BaseUIScreen screen = screens[i];
            if(screen.GetType() == typeof(T))
            {
                res = screen;
                screen.gameObject.SetActive(true);
            }
            else
            {
                screen.gameObject.SetActive(false);
            }
        }

        return (T)res;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
