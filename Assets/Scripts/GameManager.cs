using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameDuration = 20f;

	[HideInInspector] public StateManager states;
    [HideInInspector] public DeckHandler deckHandler;
    [HideInInspector] public AnimationManager animationManager;

    #region UNITY
    void Awake()
    {
        states = GetComponent<StateManager>() ?? gameObject.AddComponent<StateManager>();
        deckHandler = GetComponent<DeckHandler>() ?? gameObject.AddComponent<DeckHandler>();
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
