using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInit : BaseState
{

    public override void Initialize(params object[] arguments)
    {
        base.Initialize();
        Debug.Log("STATE INIT");
        this.game.states.Switch<StateHome>();
    }
}
