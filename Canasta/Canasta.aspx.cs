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
        List<Player> numberOfPlayers = new List<Player>();
        const int numberOfHands = 1;
        bool isSelecting = false;
        List<Card>[] hands = new List<Card>[numberOfHands];
        List<Card> playableCards = new List<Card>();
        List<Card> wildCards = new List<Card>();
        bool isPlayableSelection = true;

        protected void Page_Init(object sender, EventArgs e)
        {
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

            //<---------ShufflesDeck------------>
            if (System.Web.HttpContext.Current.Session["deck"] == null)
            {
                Random rand = new Random();
                shuffledDeck = deck.OrderBy(card => rand.Next()).ToList();

                for (int x = 0; x < hands.Length; x++)
                {
                    hands[x] = new List<Card>();
                    // Transfers the last 15 cards from the shuffled deck to your hand
                    for (int y = 0; y < 15; y++)
                    {
                        hands[x].Add(shuffledDeck[shuffledDeck.Count - 1]);
                        shuffledDeck.RemoveAt(shuffledDeck.Count - 1);
                    }
                    Session.Add("deck", shuffledDeck);
                    Session.Add("hand" + (x + 1), hands[x]);
                }
            }
            else
            {
                shuffledDeck = (List<Card>)System.Web.HttpContext.Current.Session["deck"];
                for (int x = 0; x < hands.Length; x++)
                {
                    hands[x] = (List<Card>)System.Web.HttpContext.Current.Session["hand" + (x + 1)];
                }
            }

            //<---------Creates Discard Pile------------>
            discardPile.Add(shuffledDeck.First());
            DiscardPileImage.ImageUrl = "Images/" + discardPile.First().CardValue + discardPile.First().CardSuit + ".png";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //int[] playerScore = new int[numberOfHands];
            for (int i = 0; i < 4; i++)
            {
                numberOfPlayers.Add(new Player("Player " + i.ToString()));
            }
            for (int x = 0; x < hands.Count(); x++)
            {
                DisplayPlayerCards(hands[x]);
            }
            PickUpPileLabel.Text = shuffledDeck.Count.ToString();
        }

        //<---------Selects card when clicked------------>
        //<---------Calls DisplayPlayerCards method------------>
        void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton clickedCardImage = new ImageButton();
            clickedCardImage = (ImageButton)sender;

            System.Web.UI.HtmlControls.HtmlGenericControl cardDiv =
                (System.Web.UI.HtmlControls.HtmlGenericControl)clickedCardImage.Parent;

            ControlCollection divList = TestImages.Controls;
            int clickedCardIndex = divList.IndexOf(cardDiv) - 3;

            List<Card> hand = new List<Card>();
            hand = (List<Card>)Session["hand1"];

            if (hand[clickedCardIndex].IsSelected == false)
            {
                hand[clickedCardIndex].IsSelected = true;
            }
            else
            {
                hand[clickedCardIndex].IsSelected = false;
            }

            Session.Add("hand1", hand);

            isSelecting = true;
            DisplayPlayerCards(hand);

            Response.Redirect(Request.RawUrl);
        }

        void DisplayPlayerCards(List<Card> hand)
        {
            int[] playerScore = new int[numberOfHands];
            for (int x = 0; x < hands.Length; x++)
            {
                playerScore[x] = 0;

                foreach (Card card in hands[x])
                {
                    playerScore[x] = playerScore[x] + card.PointValue;
                }

                TextBox textbox = new TextBox
                {
                    ID = "Player" + (x + 1) + "Score",
                    Text = playerScore[x].ToString()
                };

                TestImages.Controls.Add(textbox);
                TestImages.Controls.Add(new LiteralControl("<br />"));
            }


            //Displays your HAND
            int cardIndex = 0;
            List<System.Web.UI.HtmlControls.HtmlGenericControl> cardDivs = new List<System.Web.UI.HtmlControls.HtmlGenericControl>();
            List<Int32> selectedCardIndices = new List<int>();


            foreach (Card card in hand)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl cardDiv =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("div");

                cardDiv.ID = "Player1Card" + (cardIndex + 1) + "Container";
                cardDiv.Attributes.Add("class", "cardDiv");

                if (card.IsSelected == true)
                {
                    cardDiv.Attributes.Add("class", "cardDiv cardDivSelected");
                    //selectedCards.Add(card);
                    selectedCardIndices.Add(cardIndex);
                    //cardDiv.Style.Add("background-color", "red");
                }

                //cardDiv.Style.Add("border-style", "solid");

                ImageButton image = new ImageButton
                {
                    ID = "Player1Card" + (cardIndex + 1),
                    ImageUrl = "Images/" + card.CardValue + card.CardSuit + ".png",
                    //Height = 100,
                    CssClass = "imageButton"
                };

                image.Click += new ImageClickEventHandler(ImageButton_Click);


                //TestImages.Controls.Add(cardDiv);
                cardDiv.Controls.Add(image);
                cardDivs.Add(cardDiv);

                cardIndex++;

                //playerScore[playerIndex] = playerScore[playerIndex] + card.PointValue;

            }

            if (selectedCardIndices.Count < 3)
            {
                isPlayableSelection = false;
            }
            else
            {
                foreach (int selectedCardIndex in selectedCardIndices)
                {
                    if (hand[selectedCardIndex].CardValue == Card.Value.Three)
                    {
                        isPlayableSelection = false;
                        break;
                    }
                    else if (hand[selectedCardIndex].CardValue == Card.Value.Two || hand[selectedCardIndex].CardValue == Card.Value.Joker)
                    {
                        wildCards.Add(hand[selectedCardIndex]);
                    }
                    else
                    {
                        playableCards.Add(hand[selectedCardIndex]);
                    }
                }

                if (isPlayableSelection == true)
                {
                    if (wildCards.Count >= playableCards.Count)
                    {
                        isPlayableSelection = false;
                    }
                    else
                    {
                        for (int x = 1; x < playableCards.Count; x++)
                        {
                            if (playableCards[0].CardValue != playableCards[x].CardValue)
                            {
                                isPlayableSelection = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (isPlayableSelection == false)
            {
                foreach (int selectedCardIndex in selectedCardIndices)
                {
                    cardDivs[selectedCardIndex].Attributes.Add("class", "cardDiv playableFalse");
                }
            }
            else
            {
                foreach (int selectedCardIndex in selectedCardIndices)
                {
                    cardDivs[selectedCardIndex].Attributes.Add("class", "cardDiv playableTrue");
                }
            }

            foreach (System.Web.UI.HtmlControls.HtmlGenericControl cardDiv in cardDivs)
            {
                TestImages.Controls.Add(cardDiv);
            }
        }

        //<---------Button Click Methods------------>
        protected void PickUpPileImage_Click(object sender, ImageClickEventArgs e)
        {
            hands[0].Add(shuffledDeck[shuffledDeck.Count - 1]);
            shuffledDeck.RemoveAt(shuffledDeck.Count - 1);
        }

        protected void MeldButton_Click(object sender, EventArgs e)
        {
            int cardIndex = 0;
            List<Card> meld = new List<Card>();
            if (isPlayableSelection == true)
            {
                foreach (Card card in playableCards)
                {
                    meld.Add(card);
                    hands[0].Remove(card);
                    Image image = new Image
                    {
                        ID = "Player1Meld" + (cardIndex + 1),
                        ImageUrl = "Images/" + card.CardValue + card.CardSuit + ".png",
                        Height = 100
                    };
                }
            }
        }

        protected void DiscardPileImage_Click(object sender, ImageClickEventArgs e)
        {
            hands[0].Add(discardPile.First());
            discardPile.Clear();
        }
    }
}