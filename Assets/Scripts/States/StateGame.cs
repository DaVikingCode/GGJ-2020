using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGame : BaseState
{
    UIGame uigame;

	public int cardUsed = 0;
	public CardData currentCard;
	public Spell currentSpell;
	public Spell targetSpell;

	public override void Initialize()
    {
        base.Initialize();
        uigame = UIManager.instance.SwitchScreen<UIGame>();

		targetSpell = game.deckHandler.getRandomSpell();
		currentSpell = game.deckHandler.getRandomSpell();
		currentCard = game.deckHandler.getRandomCard();


        uigame.card.SetSymbol(currentCard.front);
        uigame.card.PopCard();
    }

    private void UpdateColors()
    {
        uigame.SetCurrent(currentSpell.hue, currentSpell.lightness);
        //Changer la target ici avec uigame.SetTarget
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			cardUsed++;
			CardData card = game.deckHandler.currentCard;
			bool isFinished = game.deckHandler.goToNextCard();
			if (isFinished)
				this.game.stateManager.SwitchToState<StateEnd>();
		} else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			bool isFinished = game.deckHandler.goToNextCard();
			if (isFinished)
				this.game.stateManager.SwitchToState<StateEnd>();
		}
		
	}
}
