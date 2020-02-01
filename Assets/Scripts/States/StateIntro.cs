using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntro : BaseState
{

    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("STATE INTRO - todo : card fan animation");
        this.game.stateManager.SwitchToState<StateGame>();
    }
}
