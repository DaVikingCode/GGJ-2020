using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

	[HideInInspector] public StateManager states;
    [HideInInspector] public DeckHandler deckHandler;
	[HideInInspector] public ResourceManager resourceManager;
    [HideInInspector] public AnimationManager animationManager;

    #region UNITY
    void Awake()
    {
        states = GetComponent<StateManager>() ?? gameObject.AddComponent<StateManager>();
        deckHandler = GetComponent<DeckHandler>() ?? gameObject.AddComponent<DeckHandler>();
		resourceManager = GetComponent<ResourceManager>() ?? gameObject.AddComponent<ResourceManager>();
        animationManager = GetComponent<AnimationManager>() ?? gameObject.AddComponent<AnimationManager>();

        instance = this;
    }

    private void Start()
    {
        deckHandler.Initialize();
        states.Initialize();

        states.Switch<StateInit>();

    }
    #endregion

}
