using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGame : BaseState
{
    UIGame gameScreen;

    public override void Initialize()
    {
        base.Initialize();
        gameScreen = UIManager.instance.SwitchScreen<UIGame>();
    }
}
