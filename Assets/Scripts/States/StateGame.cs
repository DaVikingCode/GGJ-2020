using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIGame;

public class StateGame : BaseState
{
    UIGame uigame;

	public int cardUsed = 0;
	public CardData currentCard;
	public Spell currentSpell;
	public Spell targetSpell;

    float timer = 0f;
	
	public override void Initialize(params object[] arguments)
    {
        base.Initialize();
        uigame = UIManager.instance.SwitchScreen<UIGame>();

        timer = this.game.gameDuration;


        targetSpell = game.deckHandler.getRandomSpell();
		currentSpell = game.deckHandler.getRandomSpell();

		while( currentSpell.hue == targetSpell.hue )
		{
			currentSpell = game.deckHandler.getRandomSpell();
		}

		currentCard = game.deckHandler.getRandomCard();

		uigame.slider.normalizedValue = 0;

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
        //uigame.SetCurrent(currentSpell.hue, currentSpell.lightness);
        uigame.SetCurrent2(currentSpell, currentCard );

        uigame.SetTarget(targetSpell.hue, targetSpell.lightness);
    }

    IEnumerator End(bool success)
    {
        while (uigame.animatingColors)
            yield return null;

        this.game.states.Switch<StateEnd>(currentSpell, targetSpell, success);
    }

	private void Update()
	{
		timer -= Time.deltaTime;
		uigame.slider.normalizedValue = Mathf.Max((this.game.gameDuration - timer) / this.game.gameDuration, 0);


        //DO NOTHING IF ANIMATING BACKGROUND COLORS
        if (uigame.animatingColors)
            return;

        if (timer < 0)
		{
            StartCoroutine(End(false));
            return;
		}

        //DO NOTHING IF CARD IS ANIMATING
        if (uigame.card.isAnimating)
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
			UpdateColors();
			currentSpell.AddCardValues(currentCard);
		}
        

        if (currentSpell.hue % 360 == targetSpell.hue && currentSpell.lightness == targetSpell.lightness)
            StartCoroutine(End(true));
        else
        {
			game.deckHandler.goToNextCard();
            currentCard = game.deckHandler.currentCard;
            ShowCurrentCard();
        }
    }

}
