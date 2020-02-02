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
	public float timer = 20; 

	public override void Initialize(params object[] arguments)
    {
        base.Initialize();
        uigame = UIManager.instance.SwitchScreen<UIGame>();

		targetSpell = game.deckHandler.getRandomSpell();
		currentSpell = game.deckHandler.getRandomSpell();
		currentCard = game.deckHandler.getRandomCard();

		timer = 20;

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
		timer -= Time.deltaTime;
		if(timer < 0)
		{
			this.game.states.Switch<StateEnd>(currentSpell, false);
		}

        //DO NOTHING IF CARD IS ANIMATING
        if (uigame.card.isAnimating)
            return;

        //DO NOTHING IF ANIMATING BACKGROUND COLORS
        if (uigame.animatingColors)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
		{
            game.deckHandler.takeCard();
            uigame.card.SwipeRight(() => EndCardSwipe(true));

        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
            game.deckHandler.disposeCard();
            uigame.card.SwipeLeft(() => EndCardSwipe(false));
        }

		
	}

    void EndCardSwipe(bool picked)
    {
		if (picked)
		{
			cardUsed++;
			currentSpell.AddCardValues(currentCard);
			UpdateColors();
		}
        

        if (currentSpell.hue == targetSpell.hue && currentSpell.lightness == targetSpell.lightness)
            this.game.states.Switch<StateEnd>(currentSpell, true);
        else
        {
			game.deckHandler.goToNextCard();
            currentCard = game.deckHandler.currentCard;
            ShowCurrentCard();
        }
    }

}
