using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

	public StateManager stateManager;
    public DeckHandler deckHandler;

    #region UNITY
    void Awake()
    {
        stateManager = GetComponent<StateManager>();
        if (stateManager == null)
        {
            stateManager = this.gameObject.AddComponent<StateManager>();
        }

        deckHandler = GetComponent<DeckHandler>();
        if (deckHandler == null)
        {
            deckHandler = this.gameObject.AddComponent<DeckHandler>();
        }

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
