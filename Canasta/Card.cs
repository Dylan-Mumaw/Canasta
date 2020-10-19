using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canasta
{
    public class Card
    {
        public enum Value
        {
            Joker,
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }

        public enum Suit
        {
            Clubs,
            Spades,
            Hearts,
            Diamonds
        }

        private Value cardValue;
        private Suit cardSuit;
        private int pointValue;
        private bool isSelected;

        public Card(Value cardValue, Suit cardSuit)
        {
            this.cardValue = cardValue;
            this.CardSuit = cardSuit;
            pointValue = ComputeScore();
            this.isSelected = false;
        }

        public Value CardValue
        {
            get
            {
                return this.cardValue;
            }
            set
            {
                this.cardValue = value;
                pointValue = ComputeScore();
            }
        }

        public Suit CardSuit
        {
            get
            {
                return this.cardSuit;
            }
            set
            {
                this.cardSuit = value;
                pointValue = ComputeScore();
            }
        }

        public int PointValue
        {
            get
            {
                return this.pointValue;
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
            }
        }

        private int ComputeScore()
        {
            if ((this.CardValue.Equals(Card.Value.Three) && (this.CardSuit.Equals(Card.Suit.Clubs) || this.CardSuit.Equals(Card.Suit.Spades))) || this.CardValue.Equals(Card.Value.Four) || this.CardValue.Equals(Card.Value.Five) || this.CardValue.Equals(Card.Value.Six) || this.CardValue.Equals(Card.Value.Seven))
            {
                return 5;
            }
            else if (this.CardValue.Equals(Card.Value.Eight) || this.CardValue.Equals(Card.Value.Nine) || this.CardValue.Equals(Card.Value.Ten) || this.CardValue.Equals(Card.Value.Jack) || this.CardValue.Equals(Card.Value.Queen) || this.CardValue.Equals(Card.Value.King))
            {
                return 10;
            }
            else if (this.CardValue.Equals(Card.Value.Two) || this.CardValue.Equals(Card.Value.Ace))
            {
                return 20;
            }
            else if (this.CardValue.Equals(Card.Value.Joker))
            {
                return 50;
            }
            else if (this.CardValue.Equals(Card.Value.Three) && (this.CardSuit.Equals(Card.Suit.Hearts) || this.CardSuit.Equals(Card.Suit.Diamonds)))
            {
                return 100;
            }
            else
            {
                return 0;
            }
        }
    }
}