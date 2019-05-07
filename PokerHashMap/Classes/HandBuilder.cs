using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHashMap.Classes
{
    public class HandBuilder
    {
        /// <summary>
        /// Creates all possible hands for ThreeOfAKind
        /// </summary>
        public void DefineThreeOfAKind()
        {
            string[] cards = new string[13];
            cards[0] = "2";
            cards[1] = "3";
            cards[2] = "4";
            cards[3] = "5";
            cards[4] = "6";
            cards[5] = "7";
            cards[6] = "8";
            cards[7] = "9";
            cards[8] = "0"; //To keep each card on character, tens are displayed as zero
            cards[9] = "j";
            cards[10] = "q";
            cards[11] = "k";
            cards[12] = "a";
            string hand = "";
            int current = 12; //Starting at Ace
            while (current >= 0)
            {
                string startHand = cards[current] + cards[current] + cards[current]; //Builds hand around best base hand
                for (int i = cards.Length - 1; i >= 0; i--)
                {
                    for (int j = i; j >= 0; j--)
                    {
                        if (j == current || i == current || j == i) //Skip these hands since they aren't ThreeOfAKind
                            continue;
                        hand = startHand;
                        if (current < j && current < i)
                        {
                            if (j < i)
                                hand = hand + cards[j] + cards[i];
                            if (j > i)
                                hand = hand + cards[i] + cards[j];
                        }
                        else if (current > j && current < i)
                            hand = cards[j] + hand + cards[i];
                        else if (current > j && current > i)
                        {
                            if (j < i)
                                hand = cards[j] + cards[i] + hand;
                            if (j > i)
                                hand = cards[i] + cards[j] + hand;
                        }
                        if (hand.Length == 3) //Similar to continue above, no valid hand created
                            continue;
                        Console.WriteLine(hand);
                    }
                }
                current--; //Move to next base hand
            }
        }

        /// <summary>
        /// Creates all possible hands for TwoPair
        /// </summary>
        public void DefineTwoPair()
        {
            string[] cards = new string[13];
            cards[0] = "2";
            cards[1] = "3";
            cards[2] = "4";
            cards[3] = "5";
            cards[4] = "6";
            cards[5] = "7";
            cards[6] = "8";
            cards[7] = "9";
            cards[8] = "0"; //To keep each card on character, tens are displayed as zero
            cards[9] = "j";
            cards[10] = "q";
            cards[11] = "k";
            cards[12] = "a";
            string hand = "";
            int current = 12; //Starting at Ace
            while (current >= 0)
            {
                string startHand = cards[current] + cards[current]; //Builds hand around best base hand
                for (int i = cards.Length - 1; i >= 0; i--)
                {
                    for (int j = cards.Length - 1; j >= 0; j--)
                    {
                        if (j == current || i == current || j == i) //Skip these hands since they aren't TwoPairs
                            continue;
                        hand = startHand;
                        if (current < j && current < i)
                        {
                            if (j < i)
                                hand = hand + cards[j] + cards[i] + cards[i];
                            if (j > i)
                                hand = hand + cards[i] + cards[i] + cards[j];
                        }
                        else if (current > j && current < i)
                        {
                            hand = cards[j] + hand + cards[i] + cards[i];
                        }
                        else if (current > j && current > i)
                        {
                            if (j < i)
                                hand = cards[j] + cards[i] + cards[i] + hand;
                            if (j > i)
                                hand = cards[i] + cards[i] + cards[j] + hand;
                        }
                        if (hand.Length == 2) //Similar to continue above, no valid hand created
                            continue;
                        Console.WriteLine(hand);
                    }
                }
                current--; //Move to next base hand
            }
        }
    }
}
