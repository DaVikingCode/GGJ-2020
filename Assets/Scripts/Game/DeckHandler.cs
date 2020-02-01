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

    public List<CardData> initializeDeck()
    {
		List<CardData> initialPool = new List<CardData>();
		List<CardData> remainingCards = new List<CardData>();

		Debug.Log("Cards counts : " + cardsSO.cards.ToList().Count);

		while (currentSpell == null)
		{
			deck = new List<CardData>();
			List<CardData>[] result = this.getRandomCardsFromList(cardsSO.cards.ToList(), 4);
			initialPool = result[0];
			remainingCards = result[1];
			deck.AddRange(initialPool);

			List<SpellData> availableSpells = this.findPlayableSpells(deck);
			if(availableSpells.Count > 0)
			{
				currentSpell = availableSpells[Random.Range(0, availableSpells.Count)];
			}
		}

		List<CardData> filler = this.getRandomCardsFromList(remainingCards, 3)[0];
		deck.AddRange(filler);

		return deck;
    }

    List<SpellData> findPlayableSpells(List<CardData> deck)
    {
		List<SpellData> availableSpells = new List<SpellData>();

		foreach ( SpellData spell in spellsSO.spells )
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
			if (spell.minA < a && a < spell.maxA
				&& spell.minB < b && b < spell.maxB
				&& spell.minC < c && c < spell.maxC)
			{
				availableSpells.Add(spell);
			};
		}

		return availableSpells;
    }

}
