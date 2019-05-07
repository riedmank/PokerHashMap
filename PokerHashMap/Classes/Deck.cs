using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PokerHashMap.Classes
{
    public class Deck<T> : IEnumerable
    {
        public int Length { get { return counter; } }
        T[] deck = new T[5];
        int counter = 0;

        /// <summary>
        /// Creates an indexer
        /// </summary>
        /// <param name="i">Index</param>
        /// <returns>returns value at index</returns>
        public T this[int i]
        {
            get { return deck[i]; }
            set { deck[i] = value; }
        }

        /// <summary>
        /// Add a card to the deck collection
        /// </summary>
        /// <param name="card">Takes in the card to be added</param>
        public void Add(T card)
        {
            if (counter == deck.Length)
                Array.Resize(ref deck, deck.Length * 2);
            deck[counter++] = card;
        }

        /// <summary>
        /// Removes a card from the deck
        /// </summary>
        /// <param name="card">The card to be removed</param>
        public void Remove(T card)
        {
            bool check = deck[0].Equals(card);
            for (int i = 0; i < counter; i++)
            {
                if (!check)
                {
                    deck[i] = deck[i];
                    if (i + 1 == counter)
                        return;
                    else
                        check = deck[i + 1].Equals(card);
                }
                else
                    deck[i] = deck[i + 1];
            }
            counter--;
        }

        /// <summary>
        /// Creates a new deck and puts only the cards with a specific suit in the deck
        /// </summary>
        /// <param name="suit">Takes in a suite</param>
        /// <returns>Returns a deck of only one suit</returns>
        public Deck<Card> ShowSuit(Suit suit)
        {
            Deck<Card> DeckToReturn = new Deck<Card>();
            for (int i = 0; i < counter; i++)
            {
                Card temp = (Card)Convert.ChangeType(deck[i], typeof(Card));
                if (temp.Suit == suit)
                    DeckToReturn.Add(temp);
            }
            return DeckToReturn;
        }

        /// <summary>
        /// Finds a card in the deck with respect to value only
        /// </summary>
        /// <param name="hand">Hand of cards</param>
        /// <param name="target">Target card</param>
        /// <returns>True or false</returns>
        public bool FindCardInDeck(Deck<Card> hand, Value target)
        {
            foreach (Card card in hand)
            {
                if (card.Value == target)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Finds a card in the deck including Suit
        /// </summary>
        /// <param name="hand">Hand of cards</param>
        /// <param name="target">Target card</param>
        /// <returns>True or false</returns>
        public bool FindCardInDeckWithSuit(Deck<Card> hand, Suit suitTarget, Value valueTarget)
        {
            foreach (Card card in hand)
            {
                if (card.Suit == suitTarget && card.Value == valueTarget)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Finds a card in the deck by index
        /// </summary>
        /// <param name="index">Index of the card</param>
        /// <returns>The card or default</returns>
        public T FindCardInDeckByIndex(int index)
        {
            if (index > counter)
                return default(T);
            else
                return deck[index];
        }

        /// <summary>
        /// Allows for the foreach construct to be used by Deck class
        /// </summary>
        /// <returns>Every Card in Deck</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < counter; i++)
            {
                yield return deck[i];
            }
        }

        /// <summary>
        /// Required by IEnumerable interface
        /// </summary>
        /// <returns>GetEnumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
