using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckHandler : MonoBehaviour
{
    public CardsScriptableObject cardsSO;
    
    public Spell currentSpell = null;
    public List<CardData> deck = new List<CardData>();
    public CardData currentCard = null;
	private int _currentIndex = 0;

	public void Initialize()
    {
        cardsSO = Resources.Load<CardsScriptableObject>("Cards");
    }

    Spell getRandomSpell()
    {
		int lightness = cardsSO.minLightness + cardsSO.lightnessStep * Random.Range(0, (cardsSO.maxLightness - cardsSO.minLightness) / cardsSO.lightnessStep);
		int hue = cardsSO.minHue + cardsSO.hueStep * Random.Range(0, (cardsSO.maxHue - cardsSO.minHue) / cardsSO.hueStep);
		return new Spell(hue, lightness);
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

	public bool goToNextCard()
	{
		_currentIndex++;
		if (_currentIndex < deck.Count)
		{
			currentCard = deck[_currentIndex];
			return false;
		}
		return true;
		
	}

}
