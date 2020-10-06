using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Canasta
{
    public partial class Canasta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Card> deck = new List<Card>();
            int playerScore = 0;

            // 4 players play with 5 decks of cards including 2 jokers in each
            for (int x = 0; x < 5; x++)
            {
                // Arbitrarily decided red joker is diamonds and black joker is spades
                Card jokerCard = new Card(Card.Value.Joker, Card.Suit.Diamonds);
                deck.Add(jokerCard);
                jokerCard = new Card(Card.Value.Joker, Card.Suit.Spades);
                deck.Add(jokerCard);

                foreach (Card.Suit cardSuit in Enum.GetValues(typeof(Card.Suit)))
                {
                    foreach (Card.Value cardValue in Enum.GetValues(typeof(Card.Value)))
                    {
                        if (cardValue == Card.Value.Joker)
                        {
                            continue;
                        }

                        Card card = new Card(cardValue, cardSuit);
                        deck.Add(card);
                    }
                }
            }

            // Shuffles deck
            Random rand = new Random();
            List<Card> shuffledDeck = deck.OrderBy(card => rand.Next()).ToList();

            // Displays shuffled deck

            /*foreach (Card card in shuffledDeck)
            {
                Image image = new Image();
                image.ImageUrl = "Images/" + card.CardValue + card.CardSuit + ".png";
                image.Height = 100;
                TestImages.Controls.Add(image);
            }*/


            List<Card> yourHand = new List<Card>();

            // Transfers the last 15 cards from the shuffled deck to your hand
            for (int x = 0; x < 15; x++)
            {
                yourHand.Add(shuffledDeck[shuffledDeck.Count - 1]);
                shuffledDeck.RemoveAt(shuffledDeck.Count - 1);
            }

            // Displays your hand
            foreach (Card card in yourHand)
            {
                Image image = new Image();
                image.ImageUrl = "Images/" + card.CardValue + card.CardSuit + ".png";
                image.Height = 100;
                TestImages.Controls.Add(image);                
                playerScore = playerScore + ComputeScore(card, playerScore);
                TextBox1.Text = playerScore.ToString();
            }           
        }
        public static int ComputeScore(Card card, int playerScore)
        {
            playerScore = 0;           
            if (card.CardValue.Equals(Card.Value.Three) || card.CardValue.Equals(Card.Value.Four) || card.CardValue.Equals(Card.Value.Five) || card.CardValue.Equals(Card.Value.Six) || card.CardValue.Equals(Card.Value.Seven))
            {
                playerScore = playerScore + 5;
                return playerScore;
            }
            else if (card.CardValue.Equals(Card.Value.Eight) || card.CardValue.Equals(Card.Value.Nine) || card.CardValue.Equals(Card.Value.Ten) || card.CardValue.Equals(Card.Value.Jack) || card.CardValue.Equals(Card.Value.Queen) || card.CardValue.Equals(Card.Value.King))
            {
                playerScore = playerScore + 10;
                return playerScore;
            }
            else if (card.CardValue.Equals(Card.Value.Two) || card.CardValue.Equals(Card.Value.Ace))
            {
                playerScore = playerScore + 20;
                return playerScore;
            }
            else
            {
                return 0;
            }
        }
    }
}