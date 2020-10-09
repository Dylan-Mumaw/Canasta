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
        List<Card> deck = new List<Card>();
        List<Card> yourHand = new List<Card>();
        //List<Card> pickUpPile = new List<Card>();
        List<Card> shuffledDeck = new List<Card>();
        List<Card> discardPile = new List<Card>();
        protected void Page_Load(object sender, EventArgs e)
        {
            
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

            //List<Card> shuffledDeck = new List<Card>();

            if (System.Web.HttpContext.Current.Session["deck"] == null)
            {
                // Shuffles deck
                Random rand = new Random();
                shuffledDeck = deck.OrderBy(card => rand.Next()).ToList();

                // Transfers the last 15 cards from the shuffled deck to your hand
                for (int x = 0; x < 15; x++)
                {
                    yourHand.Add(shuffledDeck[shuffledDeck.Count - 1]);
                    shuffledDeck.RemoveAt(shuffledDeck.Count - 1);
                }

                Session.Add("deck", shuffledDeck);
                Session.Add("hand", yourHand);
            }
            else
            {
                shuffledDeck = (List<Card>)System.Web.HttpContext.Current.Session["deck"];
                yourHand = (List<Card>)System.Web.HttpContext.Current.Session["hand"];
            }

            // Displays shuffled deck

            /*foreach (Card card in shuffledDeck)
            {
                Image image = new Image();
                image.ImageUrl = "Images/" + card.CardValue + card.CardSuit + ".png";
                image.Height = 100;
                TestImages.Controls.Add(image);
            }*/
           
            //Pickup Pile may not be needed, able to use shuffled deck
            //Moves remainder of shuffled deck to pick-up pile
            //for (int x = 0; x < shuffledDeck.Count; x++)
            //{
            //    pickUpPile.Add(shuffledDeck[shuffledDeck.Count - 1]);
            //}
            PickUpPileLabel.Text = shuffledDeck.Count.ToString();

            // Displays your hand
            foreach (Card card in yourHand)
            {
                ImageButton image = new ImageButton();

                image.ImageUrl = "Images/" + card.CardValue + card.CardSuit + ".png";
                image.Height = 100;
                TestImages.Controls.Add(image);
                playerScore = playerScore + card.PointValue;
                TextBox1.Text = playerScore.ToString();
            }
            discardPile.Add(shuffledDeck.First());
            DiscardPileImage.ImageUrl = "Images/" + discardPile.First().CardValue + discardPile.First().CardSuit + ".png";
            
        }

        protected void PickUpPileImage_Click(object sender, ImageClickEventArgs e)
        {
            yourHand.Add(shuffledDeck[shuffledDeck.Count - 1]);
            shuffledDeck.RemoveAt(shuffledDeck.Count - 1);
            //yourHand.Add(pickUpPile[pickUpPile.Count - 1]);
            //pickUpPile.RemoveAt(pickUpPile.Count - 1);
        }
    }
}