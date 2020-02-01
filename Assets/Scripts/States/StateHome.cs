using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StateHome : BaseState
{
    UIHome home;

    public override void Initialize()
    {
        base.Initialize();

       home = UIManager.instance.SwitchScreen<UIHome>();
       home.playBtn.onClick.AddListener(OnPlayButton);
    }

    void OnPlayButton()
    {
        this.game.stateManager.SwitchToState<StateIntro>();
    }

    override public void OnDestroy()
    {
        home.playBtn.onClick.AddListener(OnPlayButton);
    }
}
