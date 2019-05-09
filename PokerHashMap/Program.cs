using PokerHashMap.Classes;
using System;

namespace PokerHashMap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PokerHashTable pht = BuildHashMap();
            Deck<Card> deck = BuildPokerDeck();

            string[] handResults = new string[9];
            handResults[0] = "StraightFlush";
            handResults[1] = "FourOfAKind";
            handResults[2] = "FullHouse";
            handResults[3] = "Flush";
            handResults[4] = "Straight";
            handResults[5] = "ThreeOfAKind";
            handResults[6] = "TwoPair";
            handResults[7] = "Pair";
            handResults[8] = "HighCard";

            Deck<Card> playerOneHand = new Deck<Card>();
            Deck<Card> playerTwoHand = new Deck<Card>();

            int playerOneHandResult = -1;
            int playerTwoHandResult = -1;

            Console.WriteLine("Welcome to Poker");
            Console.WriteLine("Would you like to score a random hand or custom hand? (1 for custom, 2 for random)");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("PlayerOne hand:");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"Enter value of card {i + 1}: (a, 2 - 10, j, q, k)");
                    string userValue = Console.ReadLine().ToLower();
                    Console.WriteLine($"Enter suit of card {i + 1}: (c, d, h, s)");
                    string userSuit = Console.ReadLine().ToLower();

                    playerOneHand.Add(HandBuilder(userValue, userSuit));
                }
                Console.WriteLine("==============================================");
                Console.WriteLine("PlayerTwo hand:");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"Enter value of card {i + 1}: (a, 2 - 10, j, q, k)");
                    string userValue = Console.ReadLine().ToLower();
                    Console.WriteLine($"Enter suit of card {i + 1}: (c, d, h, s)");
                    string userSuit = Console.ReadLine().ToLower();

                    playerTwoHand.Add(HandBuilder(userValue, userSuit));
                }
                Sort(playerOneHand);
                Sort(playerTwoHand);

                playerOneHandResult = CheckHandFiveCards(playerOneHand, pht);
                playerTwoHandResult = CheckHandFiveCards(playerTwoHand, pht);

                GetWinner(playerOneHandResult, playerOneHand, playerTwoHandResult, playerTwoHand, pht);
            }
            else if (choice == "2")
            {
                // adds random cards to hand
                Random rng = new Random();
                int counter = 52;
                for (int j = 5; j > 0; j--)
                {
                    Card card = null;
                    while (card == null)
                    {
                        card = deck.FindCardInDeckByIndex(rng.Next(0, counter--));
                    }
                    playerOneHand.Add(card);
                    deck.Remove(card);
                }

                for (int j = 5; j > 0; j--)
                {
                    Card card = null;
                    while (card == null)
                    {
                        card = deck.FindCardInDeckByIndex(rng.Next(0, counter--));
                    }
                    playerTwoHand.Add(card);
                    deck.Remove(card);
                }
                Sort(playerOneHand);
                Sort(playerTwoHand);
                playerOneHandResult = CheckHandFiveCards(playerOneHand, pht);
                playerTwoHandResult = CheckHandFiveCards(playerTwoHand, pht);
            }
            Console.WriteLine("=================================================");
            Console.WriteLine("PlayerOne hand:");
            foreach (Card item in playerOneHand)
            {
                Console.WriteLine($"{item.Value} of {item.Suit}");
            }
            Console.WriteLine("=================================================");
            Console.WriteLine("PlayerTwo hand:");
            foreach (Card item in playerTwoHand)
            {
                Console.WriteLine($"{item.Value} of {item.Suit}");
            }

            int winner = GetWinner(playerOneHandResult, playerOneHand, playerTwoHandResult, playerTwoHand, pht);

            Console.WriteLine("=================================================");
            if (winner == 1)
            {
                Console.WriteLine("PlayerOne wins with:");
                Console.WriteLine(handResults[playerOneHandResult]);
            }
            else if (winner == 2)
            {
                Console.WriteLine("PlayerTwo wins with:");
                Console.WriteLine(handResults[playerTwoHandResult]);
            }
            else
                Console.WriteLine("Players tie. Split pot");

            Console.ReadLine();
        }

        /// <summary>
        /// Builds hashmap of poker hands
        /// </summary>
        public static PokerHashTable BuildHashMap()
        {
            PokerHashTable pht = new PokerHashTable();
            string[] hands = System.IO.File.ReadAllLines(@"C:\Users\v-krried\source\repos\PokerHashMap\PokerHashMap\hand_dictionary.txt");
            foreach (string item in hands)
            {
                pht.Add(item);
            }
            return pht;
        }

        /// <summary>
        /// Creates a standard 52 card deck
        /// </summary>
        /// <returns>Deck of cards</returns>
        public static Deck<Card> BuildPokerDeck()
        {
            Deck<Card> myDeck = new Deck<Card>();
            Enum[] suits = new Enum[4];
            suits[0] = Suit.Clubs;
            suits[1] = Suit.Diamonds;
            suits[2] = Suit.Hearts;
            suits[3] = Suit.Spades;
            Enum[] values = new Enum[13];
            values[0] = Value.Ace;
            values[1] = Value.Two;
            values[2] = Value.Three;
            values[3] = Value.Four;
            values[4] = Value.Five;
            values[5] = Value.Six;
            values[6] = Value.Seven;
            values[7] = Value.Eight;
            values[8] = Value.Nine;
            values[9] = Value.Ten;
            values[10] = Value.Jack;
            values[11] = Value.Queen;
            values[12] = Value.King;
            foreach (Suit suit in suits)
            {
                foreach (Value value in values)
                {
                    Card card = new Card(suit, value);
                    myDeck.Add(card);
                }
            }
            return myDeck;
        }

        /// <summary>
        /// Converts players hand from Deck<Card> to string
        /// </summary>
        /// <param name="hand">Hand of cards</param>
        /// <returns>Stringified hand of cards</returns>
        public static string ConvertHandToString(Deck<Card> hand)
        {
            string playerHand = "";
            foreach (Card card in hand)
            {
                if (card.Value == Value.Two)
                    playerHand = playerHand + "2";
                else if (card.Value == Value.Three)
                    playerHand = playerHand + "3";
                else if (card.Value == Value.Four)
                    playerHand = playerHand + "4";
                else if (card.Value == Value.Five)
                    playerHand = playerHand + "5";
                else if (card.Value == Value.Six)
                    playerHand = playerHand + "6";
                else if (card.Value == Value.Seven)
                    playerHand = playerHand + "7";
                else if (card.Value == Value.Eight)
                    playerHand = playerHand + "8";
                else if (card.Value == Value.Nine)
                    playerHand = playerHand + "9";
                else if (card.Value == Value.Ten)
                    playerHand = playerHand + "0";
                else if (card.Value == Value.Jack)
                    playerHand = playerHand + "j";
                else if (card.Value == Value.Queen)
                    playerHand = playerHand + "q";
                else if (card.Value == Value.King)
                    playerHand = playerHand + "k";
                else if (card.Value == Value.Ace)
                    playerHand = playerHand + "a";
            }
            return playerHand;
        }

        /// <summary>
        /// Converts user input into cards
        /// </summary>
        /// <param name="userValue">String value</param>
        /// <param name="userSuit">String suit</param>
        /// <returns>Card</returns>
        public static Card HandBuilder(string userValue, string userSuit)
        {
            Value value = new Value();
            Suit suit = new Suit();

            switch (userValue)
            {
                case "a":
                    value = Value.Ace;
                    break;
                case "2":
                    value = Value.Two;
                    break;
                case "3":
                    value = Value.Three;
                    break;
                case "4":
                    value = Value.Four;
                    break;
                case "5":
                    value = Value.Five;
                    break;
                case "6":
                    value = Value.Six;
                    break;
                case "7":
                    value = Value.Seven;
                    break;
                case "8":
                    value = Value.Eight;
                    break;
                case "9":
                    value = Value.Nine;
                    break;
                case "10":
                    value = Value.Ten;
                    break;
                case "j":
                    value = Value.Jack;
                    break;
                case "q":
                    value = Value.Queen;
                    break;
                case "k":
                    value = Value.King;
                    break;
                default:
                    Console.WriteLine("You didn't pick a correct value. Goodbye.");
                    break;
            }

            switch (userSuit)
            {
                case "c":
                    suit = Suit.Clubs;
                    break;
                case "d":
                    suit = Suit.Diamonds;
                    break;
                case "h":
                    suit = Suit.Hearts;
                    break;
                case "s":
                    suit = Suit.Spades;
                    break;
                default:
                    Console.WriteLine("You didn't pick a correct suit. Goodbye.");
                    break;
            }

            Card card = new Card(suit, value);
            return card;
        }

        /// <summary>
        /// Sorts the cards in the hand by value
        /// </summary>
        /// <param name="hand">Hand of cards</param>
        public static void Sort(Deck<Card> hand)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (hand[j].Value > hand[j + 1].Value)
                        Swap(hand, j, j + 1);
                }
            }
        }

        /// <summary>
        /// Swaps two cards in the hand
        /// </summary>
        /// <param name="hand">Hand of poker</param>
        /// <param name="x">Card</param>
        /// <param name="y">Card</param>
        public static void Swap(Deck<Card> hand, int x, int y)
        {
            Card temp = hand[x];
            hand[x] = hand[y];
            hand[y] = temp;
        }

        /// <summary>
        /// Determines what hand result a player has
        /// </summary>
        /// <param name="hand">Hand of poker</param>
        /// <param name="pht">Hashtable of hands</param>
        /// <returns>Integer of hand result</returns>
        public static int CheckHandFiveCards(Deck<Card> hand, PokerHashTable pht)
        {
            int handResult = -1;
            int rank;
            string playerHand = ConvertHandToString(hand);

            int clubs = 0;
            int diamonds = 0;
            int hearts = 0;
            int spades = 0;
            bool foundMatch = false;
            Tuple<Value?, int> match = new Tuple<Value?, int>(null, 0);

            foreach (Card card in hand)
            {
                // Count cards of each Suit and find the pair if it exists
                if (card.Suit == Suit.Clubs)
                    clubs++;
                else if (card.Suit == Suit.Diamonds)
                    diamonds++;
                else if (card.Suit == Suit.Hearts)
                    hearts++;
                else
                    spades++;
                if (!foundMatch)
                {
                    if (card.Value == match.Item1)
                    {
                        match = new Tuple<Value?, int>(card.Value, match.Item2 + 1);
                        foundMatch = true;
                    }
                    else
                        match = new Tuple<Value?, int>(card.Value, 0);
                }
            }
            // Check if hand is in HashTable
            if (pht.Contains(playerHand))
            {
                rank = pht.Find(playerHand);
                if (rank >= 1 && rank < 11) //Straight in HashTable
                    handResult = 0;
                else if (rank >= 11 && rank < 167) //FourOfAKind in HashTable
                    handResult = 1;
                else if (rank >= 167 && rank < 323) //FullHouse in HashTable 
                    handResult = 2;
                else if (rank >= 323 && rank < 1182) //ThreeOfAKind in HashTable
                    handResult = 5;
                else if (rank >= 1182) //TwoPair in HashTable
                    handResult = 6;
                else
                    handResult = -1;
                // Check to see if StraightFlush is actually just a Straight
                if (handResult == 0 && (clubs < 5 && diamonds < 5 && hearts < 5 && spades < 5))
                    handResult = 4;
            }
            else
            {
                if (clubs == 5 || diamonds == 5 || hearts == 5 || spades == 5)
                    handResult = 3; //Check for Flush
                else if (match.Item2 == 1)
                    handResult = 7; //Check for Pair
                else
                    handResult = 8; //Must be HighCard if nothing else
            }
            return handResult;
        }

        /// <summary>
        /// Determines the winner of the poker hand
        /// </summary>
        /// <param name="playerOneResult">Result of scoring hand</param>
        /// <param name="playerOneHand">Poker hand</param>
        /// <param name="playerTwoResult">Result of scoring hand</param>
        /// <param name="playerTwoHand">Poker hand</param>
        /// <param name="pht">Hashtable</param>
        /// <returns>Winner of this poker hand</returns>
        public static int GetWinner(int playerOneResult, Deck<Card> playerOneHand, int playerTwoResult, Deck<Card> playerTwoHand, PokerHashTable pht)
        {
            int winner = 0;
            //Since best hand is index zero, we check for the lowest result to determine the winner
            if (playerOneResult < playerTwoResult) //player 1 has better hand
                winner = 1;
            else if (playerOneResult > playerTwoResult) //player 2 has better hand
                winner = 2;
            else
            {
                string playerOneStringHand = ConvertHandToString(playerOneHand); //Convert to string to check hand's rank in the hashtable
                string playerTwoStringHand = ConvertHandToString(playerTwoHand);
                if (pht.Contains(playerOneStringHand) && pht.Contains(playerTwoStringHand)) //If hand is in hashtable, check its rank
                {
                    if (pht.Find(playerOneStringHand) < pht.Find(playerTwoStringHand))
                        winner = 1;
                    else if (pht.Find(playerOneStringHand) > pht.Find(playerTwoStringHand))
                        winner = 2;
                }
                else
                {
                    Card playerOnePair = null;
                    Card playerTwoPair = null;
                    for (int i = 0; i < 4; i++) // find pairs if they exist
                    {
                        if (playerOneHand[i] == playerOneHand[i + 1])
                            playerOnePair = playerOneHand[i];
                        if (playerTwoHand[i] == playerTwoHand[i + 1])
                            playerTwoPair = playerTwoHand[i];
                    }
                    if (playerOnePair != null && playerTwoPair != null)
                    {
                        if (playerOnePair.Value > playerTwoPair.Value)
                            winner = 1;
                        if (playerOnePair.Value < playerTwoPair.Value)
                            winner = 2;
                    }
                    if (playerOnePair == playerTwoPair || (playerOnePair == null && playerTwoPair == null))
                    {
                        for (int i = 4; i >= 0; i--) //Check for Highest Card
                        {
                            if (playerOnePair == null && playerTwoPair == null)
                            {
                                if (playerOneHand[i].Value > playerTwoHand[i].Value)
                                {
                                    winner = 1;
                                    break;
                                }
                                if (playerOneHand[i].Value < playerTwoHand[i].Value)
                                {
                                    winner = 2;
                                    break;
                                }
                            }
                            else
                            {
                                if (playerOneHand[i].Value == playerOnePair.Value && playerTwoHand[i].Value == playerTwoPair.Value)
                                    continue;
                                if (playerOneHand[i].Value > playerTwoHand[i].Value)
                                {
                                    winner = 1;
                                    break;
                                }
                                if (playerOneHand[i].Value < playerTwoHand[i].Value)
                                {
                                    winner = 2;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return winner;
        }
    }
}
