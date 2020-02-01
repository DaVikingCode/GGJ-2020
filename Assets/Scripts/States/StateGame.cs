using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGame : BaseState
{
    UIGame uigame;

	public int cardUsed = 0;

	public override void Initialize()
    {
        base.Initialize();
        uigame = UIManager.instance.SwitchScreen<UIGame>();

		this.game.deckHandler.initializeDeck();
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			cardUsed++;
			CardData card = game.deckHandler.currentCard;
			game.resourceManager.addResource('a', card.a);
			uigame.SetResourceDisplay(0, card.a);
			game.resourceManager.addResource('b', card.b);
			uigame.SetResourceDisplay(1, card.b);
			game.resourceManager.addResource('c', card.c);
			uigame.SetResourceDisplay(2, card.c);
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
