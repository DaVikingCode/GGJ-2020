using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckHandler : MonoBehaviour
{
    public SpellsScriptableObject spellsSO;
    public CardsScriptableObject cardsSO;

    public SpellData currentSpell = null;
    public List<CardData> deck = new List<CardData>();
    public CardData currentCard = null;

    public void Initialize()
    {
        spellsSO = Resources.Load<SpellsScriptableObject>("Spells");
        cardsSO = Resources.Load<CardsScriptableObject>("Cards");
    }


    SpellData getRandomSpell()
    {
        if (spellsSO != null && spellsSO.spells != null)
        {
            int randomIndex = Random.Range(0, spellsSO.spells.Length);
            return spellsSO.spells[randomIndex];
        }
        return null;
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

    public CardData[] initializeDeck()
    {
		// Pick 3 cards from pool
		List<CardData>[] result = this.getRandomCardsFromList(cardsSO.cards.ToList(), 3);
		List<CardData> initialPool = result[0];
		List<CardData> remainingCards = result[1];
		Debug.Log(initialPool);

		return null;
		// Check wich spell is possible
		// IF a least one, select a random spell form possible
		// ELSE redo
		// 
		// Fill with 2 random cards
    }

    bool checkSpellRequirements(CardData[] deck, SpellData spell)
    {
        int a = 0; int b = 0; int c = 0;

        foreach (CardData card in deck)
        {
            a += card.a;
            b += card.b;
            c += card.c;
        }

        return spell.minA < a;
    }

}
