using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnd : BaseState
{
    public Spell finalSpell;
	public bool success;

    UIEnd uiEnd;
    public override void Initialize(params object[] arguments)
    {
        finalSpell = (Spell)arguments[0];
		success = (bool)arguments[1];

        base.Initialize();

        uiEnd = UIManager.instance.SwitchScreen<UIEnd>();

        uiEnd.background.color = Utils.GetColor(finalSpell.hue % 360, finalSpell.lightness);
        uiEnd.tryAgainButton.onClick.AddListener(OnTryAgain);
    }

    void OnTryAgain()
    {
        this.game.states.Switch<StateGame>();
    }

    public override void OnDestroy()
    {
        uiEnd.tryAgainButton.onClick.RemoveAllListeners();
        base.OnDestroy();
    }
}
