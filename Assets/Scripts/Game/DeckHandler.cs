using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class DeckHandler : MonoBehaviour
{
    public SpellsScriptableObject spellsSO;
    public CardsScriptableObject cardsSO;
    
    public SpellData currentSpell = null;
    public List<CardData> deck = new List<CardData>();
    public CardData currentCard = null;
	private int _currentIndex = 0;

    public void Initialize()
    {
        spellsSO = Resources.Load<SpellsScriptableObject>("Spells");
        cardsSO = Resources.Load<CardsScriptableObject>("Cards");
    }

    SpellData getRandomSpell()
    {
        int randomIndex = UnityEngine.Random.Range(0, spellsSO.spells.Length);
        return spellsSO.spells[randomIndex];
    }

	List<CardData>[] getRandomCardsFromList(List<CardData> cards, int numberOfCards)
    {
		List<CardData> pool = new List<CardData>();

		for (int i = 0; i < numberOfCards; i++)
		{
			int randomIndex = UnityEngine.Random.Range(0, cards.Count);
			pool.Add(cards[randomIndex]);
			cards.RemoveAt(randomIndex);
		}

		return new List<CardData>[] { pool, cards };
    }

    public void initializeDeck()
    {
		List<CardData> initialPool = new List<CardData>();
		List<CardData> remainingCards = new List<CardData>();

        bool spellFound = false;

		while (!spellFound)
		{
			deck = new List<CardData>();

			List<CardData>[] result = getRandomCardsFromList(cardsSO.cards.ToList(), 4);
			initialPool = result[0];
			remainingCards = result[1];
			deck.AddRange(initialPool);

			List<SpellData> availableSpells = findPlayableSpells(deck);
            if (availableSpells.Count > 0)
            {
                currentSpell = availableSpells[UnityEngine.Random.Range(0, availableSpells.Count)];
                spellFound = true;
            }
		}

		List<CardData> filler = getRandomCardsFromList(remainingCards, 3)[0];
		deck.AddRange(filler);

		deck.Shuffle();

		_currentIndex = 0;
		currentCard = deck[0];
    }

    List<SpellData> findPlayableSpells(List<CardData> deck)
    {
		List<SpellData> availableSpells = new List<SpellData>();

		foreach (SpellData spell in spellsSO.spells)
		{
			int a = 0;
			int b = 0;
			int c = 0;

			foreach (CardData card in deck)
			{
				a += card.a;
				b += card.b;
				c += card.c;
			}
			if (spell.minA <= a && a <= spell.maxA
				&& spell.minB <= b && b <= spell.maxB
				&& spell.minC <= c && c <= spell.maxC)
				availableSpells.Add(spell);
		}

		return availableSpells;
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
