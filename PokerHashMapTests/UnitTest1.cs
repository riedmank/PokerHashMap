using System;
using Xunit;
using PokerHashMap.Classes;
using static PokerHashMap.Program;

namespace PokerHashMapTests
{
    public class UnitTest1
    {
        /// <summary>
        /// Setting up values to be used by other tests
        /// </summary>
        PokerHashTable pht = BuildHashMap();
        public Deck<Card> setupPlayerHand(int player)
        {
            Deck<Card> testHandPlayerOne = new Deck<Card>();
            Deck<Card> testHandPlayerTwo = new Deck<Card>();
            Card card0 = new Card(Suit.Hearts, Value.Three);
            Card card1 = new Card(Suit.Hearts, Value.Four);
            Card card2 = new Card(Suit.Hearts, Value.Five);
            Card card3 = new Card(Suit.Hearts, Value.Six);
            Card card4 = new Card(Suit.Hearts, Value.Seven);

            Card card5 = new Card(Suit.Clubs, Value.Ten);
            Card card6 = new Card(Suit.Diamonds, Value.Jack);
            Card card7 = new Card(Suit.Hearts, Value.Queen);
            Card card8 = new Card(Suit.Spades, Value.King);
            Card card9 = new Card(Suit.Diamonds, Value.Ace);

            testHandPlayerOne.Add(card0);
            testHandPlayerOne.Add(card1);
            testHandPlayerOne.Add(card2);
            testHandPlayerOne.Add(card3);
            testHandPlayerOne.Add(card4);
            testHandPlayerTwo.Add(card5);
            testHandPlayerTwo.Add(card6);
            testHandPlayerTwo.Add(card7);
            testHandPlayerTwo.Add(card8);
            testHandPlayerTwo.Add(card9);

            if (player == 1)
                return testHandPlayerOne;
            else
                return testHandPlayerTwo;
        }

        /// <summary>
        /// Tests that string can be converted from all number cards
        /// </summary>
        [Fact]
        public void ConvertHandToStringTest1()
        {
            Deck<Card> testHandConvertString1 = setupPlayerHand(1);
            Assert.Equal("34567", ConvertHandToString(testHandConvertString1));
        }

        /// <summary>
        /// Tests that string can be converted from all face cards
        /// </summary>
        [Fact]
        public void ConvertHandToStringTest2()
        {
            Deck<Card> testHandConvertString2 = setupPlayerHand(2);
            Assert.Equal("0jqka", ConvertHandToString(testHandConvertString2));
        }

        /// <summary>
        /// Tests that string can be converted from mix of number and face cards
        /// </summary>
        [Fact]
        public void ConvertHandToStringTest3()
        {
            Deck<Card> testHandConvertString3 = setupPlayerHand(2);
            testHandConvertString3[0] = new Card(Suit.Clubs, Value.Eight);
            testHandConvertString3[1] = new Card(Suit.Clubs, Value.Nine);
            testHandConvertString3[2] = new Card(Suit.Clubs, Value.Ten);
            testHandConvertString3[3] = new Card(Suit.Clubs, Value.Jack);
            testHandConvertString3[4] = new Card(Suit.Clubs, Value.Queen);
            Assert.Equal("890jq", ConvertHandToString(testHandConvertString3));
        }

        /// <summary>
        /// Tests that a StraightFlush can be scored
        /// </summary>
        [Fact]
        public void CheckForStraightFlush()
        {
            Deck<Card> testHandCheckStraightFlush = setupPlayerHand(1);
            Assert.Equal(0, CheckHandFiveCards(testHandCheckStraightFlush, pht));
        }

        /// <summary>
        /// Tests that a FourOfAKind can be scored
        /// </summary>
        [Fact]
        public void CheckFourOfAKind()
        {
            Deck<Card> testHand = setupPlayerHand(2);
            testHand[1].Value = Value.Ace;
            testHand[2].Value = Value.Ace;
            testHand[3].Value = Value.Ace;
            Assert.Equal(1, CheckHandFiveCards(testHand, pht));
        }

        /// <summary>
        /// Checks that a FullHouse can be scored
        /// </summary>
        [Fact]
        public void CheckFullHouse()
        {
            Deck<Card> testHand = setupPlayerHand(1);
            testHand[1].Value = Value.Three;
            testHand[2].Value = Value.Four;
            testHand[3].Value = Value.Four;
            testHand[4].Value = Value.Four;
            Assert.Equal(2, CheckHandFiveCards(testHand, pht));
        }

        /// <summary>
        /// Checks that a Flush can be scored
        /// </summary>
        [Fact]
        public void CheckFlush()
        {
            Deck<Card> testHand = setupPlayerHand(1);
            testHand[0].Value = Value.Two;
            Assert.Equal(3, CheckHandFiveCards(testHand, pht));
        }

        /// <summary>
        /// Checks that a Straight can be scored
        /// </summary>
        [Fact]
        public void CheckStraight()
        {
            Deck<Card> testHand = setupPlayerHand(2);
            Assert.Equal(4, CheckHandFiveCards(testHand, pht));
        }

        /// <summary>
        /// Checks that a ThreeOfAKind can be scored
        /// </summary>
        [Fact]
        public void CheckThreeOfAKind()
        {
            Deck<Card> testHand = setupPlayerHand(1);
            testHand[2].Value = Value.Four;
            testHand[3] = new Card(Suit.Spades, Value.Four);
            Assert.Equal(5, CheckHandFiveCards(testHand, pht));
        }

        /// <summary>
        /// Checks that a TwoPair can be scored
        /// </summary>
        [Fact]
        public void CheckTwoPair()
        {
            Deck<Card> testHand = setupPlayerHand(2);
            testHand[0].Value = Value.Jack;
            testHand[3].Value = Value.Ace;
            Assert.Equal(6, CheckHandFiveCards(testHand, pht));
        }

        /// <summary>
        /// Checks that a Pair can be scored
        /// </summary>
        [Fact]
        public void CheckPair()
        {
            Deck<Card> testHand = setupPlayerHand(1);
            testHand[4] = new Card(Suit.Spades, Value.Six);
            Assert.Equal(7, CheckHandFiveCards(testHand, pht));
        }

        /// <summary>
        /// Checks that a HighCard can be scored
        /// </summary>
        [Fact]
        public void CheckHighCard()
        {
            Deck<Card> testHand = setupPlayerHand(2);
            testHand[0].Value = Value.Two;
            Assert.Equal(8, CheckHandFiveCards(testHand, pht));
        }

        /// <summary>
        /// Tests that a player can be declared winner with a better hand
        /// </summary>
        [Fact]
        public void CheckForWinnerWithNoTie()
        {
            Deck<Card> testHandPlayerOne = setupPlayerHand(1);
            Deck<Card> testHandPlayerTwo = setupPlayerHand(2);

            Assert.Equal(1, GetWinner(CheckHandFiveCards(testHandPlayerOne, pht), testHandPlayerOne, CheckHandFiveCards(testHandPlayerTwo, pht), testHandPlayerTwo, pht));
        }

        /// <summary>
        /// Tests that a tie can be found if both players have the same hand in the hashmap
        /// </summary>
        [Fact]
        public void CheckForWinnerWithATieHandInHashMap()
        {
            Deck<Card> testHandPlayerOne = setupPlayerHand(1);
            Deck<Card> testHandPlayerTwo = setupPlayerHand(1);

            Assert.Equal(0, GetWinner(CheckHandFiveCards(testHandPlayerOne, pht), testHandPlayerOne, CheckHandFiveCards(testHandPlayerTwo, pht), testHandPlayerTwo, pht));
        }

        /// <summary>
        /// Tests that a tie can be found if both players have the same hand not found in the hashmap
        /// </summary>
        [Fact]
        public void CheckForWinnerWithATieHandNotInHashMap()
        {
            Deck<Card> testHandPlayerOne = setupPlayerHand(2);
            Deck<Card> testHandPlayerTwo = setupPlayerHand(2);
            testHandPlayerOne[3].Value = Value.Ace;
            testHandPlayerTwo[3].Value = Value.Ace;

            Assert.Equal(0, GetWinner(CheckHandFiveCards(testHandPlayerOne, pht), testHandPlayerOne, CheckHandFiveCards(testHandPlayerTwo, pht), testHandPlayerTwo, pht));
        }
    }
}
