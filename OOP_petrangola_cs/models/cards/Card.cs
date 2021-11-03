using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_petrangola_cs.models
{
    class Card : ICard
    {
        public Card(Name name, Suit suit)
        {
            Name = name;
            Suit = suit;
        }

        public string FullName
        {
            get
            {
                string value = Value.ToString();

                if (Name.Equals(Name.Re))
                {
                    value = "10";
                }

                if (Name.Equals(Name.Cavallo))
                {
                    value = "9";
                }

                if (Name.Equals(Name.Fante))
                {
                    value = "8";
                }

                if (Name.Equals(Name.Asso))
                {
                    value = "1";
                }

                return string.Concat(string.Concat(Suit.ToString().ToLower(), "_"), value);
            }
        }

        public Name Name { get; }

        public Suit Suit { get; }

        public virtual int Value => (int)Name;
    }
}
