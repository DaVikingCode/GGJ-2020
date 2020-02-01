using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInit : BaseState
{

    public bool testBool = true;

    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("STATE INIT STARTED");

        Debug.Log("CARDS COUNT : " + this.game.deckHandler.cardsSO.cards.Length);
    }
}
