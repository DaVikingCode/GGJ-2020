﻿using System.Collections;
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

        //Timer de 20s

        UpdateColors();
        ShowCurrentCard();


    }

    private void ShowCurrentCard()
    {
        uigame.card.SetSymbol(currentCard.front);
        uigame.card.PopCard();
    }

    private void UpdateColors()
    {
        uigame.SetCurrent(currentSpell.hue, currentSpell.lightness);
        uigame.SetTarget(targetSpell.hue, targetSpell.lightness);
    }

	private void Update()
	{
        //DO NOTHING IF CARD IS ANIMATING
        if (uigame.card.isAnimating)
            return;

        //DO NOTHING IF ANIMATING BACKGROUND COLORS
        if (uigame.animatingColors)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
		{
            game.deckHandler.takeCard();
            uigame.card.SwipeRight(EndCardSwipe);

        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
            game.deckHandler.disposeCard();
            uigame.card.SwipeLeft(EndCardSwipe);
        }

		
	}

    void EndCardSwipe()
    {
        cardUsed++;

        if (game.deckHandler.goToNextCard())
            this.game.stateManager.SwitchToState<StateEnd>();
        else
        {
            currentCard = game.deckHandler.currentCard;
            currentSpell.AddCardValues(currentCard);

            ShowCurrentCard();
            UpdateColors();
        }
    }

	// Check if done
}
