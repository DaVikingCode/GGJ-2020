using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

	[HideInInspector] public StateManager stateManager;
    [HideInInspector] public DeckHandler deckHandler;
	[HideInInspector] public ResourceManager resourceManager;

	#region UNITY
	void Awake()
    {
        stateManager = GetComponent<StateManager>() ?? gameObject.AddComponent<StateManager>();
        deckHandler = GetComponent<DeckHandler>() ?? gameObject.AddComponent<DeckHandler>();
		resourceManager = GetComponent<ResourceManager>() ?? gameObject.AddComponent<ResourceManager>();

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
