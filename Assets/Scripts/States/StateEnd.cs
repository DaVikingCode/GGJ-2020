using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnd : BaseState
{
    public Spell finalSpell;
    public Spell targetSpell;

    public bool success;

    UIEnd uiEnd;
    public override void Initialize(params object[] arguments)
    {
        finalSpell = (Spell)arguments[0];
        targetSpell = (Spell)arguments[1];
        success = (bool)arguments[2];

        base.Initialize();

        uiEnd = UIManager.instance.SwitchScreen<UIEnd>();

        uiEnd.current.color = Utils.GetColor(finalSpell.hue%360f, finalSpell.lightness%100f);
        uiEnd.target.color = Utils.GetColor(targetSpell.hue%360f, targetSpell.lightness%100f);
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

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.game.states.Switch<StateGame>();
		}
	}
}
