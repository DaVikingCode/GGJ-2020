using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnd : BaseState
{
    Spell finalSpell;
    UIEnd uiEnd;
    public override void Initialize(params object[] arguments)
    {
        finalSpell = (Spell)arguments[0];
        base.Initialize();

        uiEnd = UIManager.instance.SwitchScreen<UIEnd>();

        uiEnd.background.color = Utils.GetColor(finalSpell.hue, finalSpell.lightness);
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
