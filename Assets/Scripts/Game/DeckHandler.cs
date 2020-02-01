using System.Collections;
using System.Collections.Generic;
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

    CardData getRandomCardFromList(CardData[] cards)
    {
        return cards[Random.Range(0, cards.Length)];
    }

    CardData[] initializeDeck()
    {
        if (currentSpell != null)
        {
            CardData card = getRandomCardFromList(cardsSO.cards);
            deck.Add(card);

            // Check spell requirement 
            // Pick or get random card ?
        }

        return null;
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
