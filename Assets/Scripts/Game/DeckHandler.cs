using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckHandler : MonoBehaviour
{
    public CardsScriptableObject cardsSO;
    
    public Spell currentSpell = null;
    public List<CardData> deck = new List<CardData>();
    public CardData currentCard = null;

	public void Initialize()
    {
        cardsSO = Resources.Load<CardsScriptableObject>("Cards");
    }

    public void takeCard()
    {
        Debug.Log("TAKE CARD");
    }

    public void disposeCard()
    {
        Debug.Log("DISPOSE CARD");
    }

    public Spell getRandomSpell()
    {
		int lightness = Random.value > 0.5 ? 50 : 75 ;//cardsSO.minLightness + cardsSO.lightnessStep * Random.Range(0, (cardsSO.maxLightness - cardsSO.minLightness) / cardsSO.lightnessStep);
		int hue = cardsSO.minHue + cardsSO.hueStep * Random.Range(0, (cardsSO.maxHue - cardsSO.minHue) / cardsSO.hueStep);
		return new Spell(hue, lightness);
    }

	public CardData getRandomCard()
	{
		if(deck.Count < 6)
		{
			deck = cardsSO.cards.ToList();

		}
		int randomIndex = Random.Range(0, deck.Count);
		CardData card = deck[randomIndex];
		deck.RemoveAt(randomIndex);

		return card;
	}

	List<CardData>[] getRandomCardsFromList(List<CardData> cards, int numberOfCards)
    {
		List<CardData> pool = new List<CardData>();

		for (int i = 0; i < numberOfCards; i++)
		{
			int randomIndex = Random.Range(0, cards.Count);
			pool.Add(cards[randomIndex]);
			cards.RemoveAt(randomIndex);
		}

		return new List<CardData>[] { pool, cards };
    }

	public void goToNextCard()
	{
        this.currentCard = getRandomCard();
		
	}

}
