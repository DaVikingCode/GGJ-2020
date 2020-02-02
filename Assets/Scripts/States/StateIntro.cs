using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntro : BaseState
{

    public override void Initialize(params object[] arguments)
    {
        base.Initialize();
        Debug.Log("STATE INTRO - todo : card fan animation");
        this.game.states.Switch<StateGame>();
    }
}
