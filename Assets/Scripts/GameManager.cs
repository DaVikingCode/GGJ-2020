using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

	[HideInInspector] public StateManager stateManager;
    [HideInInspector] public DeckHandler deckHandler;

    #region UNITY
    void Awake()
    {
        stateManager = GetComponent<StateManager>() ?? gameObject.AddComponent<StateManager>();
        deckHandler = GetComponent<DeckHandler>() ?? gameObject.AddComponent<DeckHandler>();

        instance = this;
    }

    private void Start()
    {
        deckHandler.Initialize();
        stateManager.Initialize();

        stateManager.SwitchToState<StateInit>();

    }
    #endregion

}
