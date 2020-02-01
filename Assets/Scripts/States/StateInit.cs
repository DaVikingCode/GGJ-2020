using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInit : BaseState
{

    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("STATE INIT");
        this.game.stateManager.SwitchToState<StateHome>();
    }
}
