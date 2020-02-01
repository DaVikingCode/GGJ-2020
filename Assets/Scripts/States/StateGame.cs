using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGame : BaseState
{
    UIGame uigame;

	public int a = 0;
	public int b = 0;
	public int c = 0;

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
			CardData card = game.deckHandler.currentCard;
			a += card.a;
			b += card.b;
			c += card.c;
			game.deckHandler.goToNextCard();
		} else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			game.deckHandler.goToNextCard();
		}
		
	}
}
