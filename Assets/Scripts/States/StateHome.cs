using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHome : BaseState
{

    public override void Initialize()
    {
        base.Initialize();
        UIManager.instance.SwitchScreen<UIHome>();
    }
}
