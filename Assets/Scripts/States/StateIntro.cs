using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntro : BaseState
{
    UIIntro uiIntro;
    public override void Initialize(params object[] arguments)
    {
        base.Initialize();
        uiIntro = UIManager.instance.SwitchScreen<UIIntro>();
        uiIntro.StartAnimation(EndIntro);
    }

    void EndIntro()
    {
        this.game.states.Switch<StateGame>();
    }
}
