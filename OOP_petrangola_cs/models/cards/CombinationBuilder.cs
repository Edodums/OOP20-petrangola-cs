using System.Collections.Generic;
using System.Linq;

namespace OOP_petrangola_cs.models.cards
{
    class CombinationBuilder : ICombinationBuilder
    {
        private List<ICard> cards;

        public ICombination Build()
        {
            return new Combination(this);
        }

        public ICombinationBuilder SetCards(List<ICard> cards)
        {
            if (cards.Count == 3)
            {
                this.cards = cards;
            }
            else
            {
                throw new System.Exception("Too many cards");
            }


            return this;
        }

        public List<ICard> GetCards()
        {
            return this.cards;
        }


        private class Combination : ICombination
        {
            private readonly List<ICard> cards;

            public Combination(ICombinationBuilder builder)
            {
                this.cards = builder.GetCards();
            }

            public KeyValuePair<List<ICard>, int> GetBest()
            {
                List<ICard> cards = new List<ICard>(GetCards());

                if (ICombinationChecker.IsTris(cards))
                {
                    return new KeyValuePair<List<ICard>, int>(cards, cards[0].Value * 3);
                }

                if (ICombinationChecker.IsAceLow(cards))
                {
                    cards = cards.Where(card => card.Name.Equals(Name.Asso))
                                 .Select(card => new AceLow(card.Name, card.Suit))
                                 .Cast<ICard>()
                                 .ToList();


                }

                int bestValue = cards.GroupBy(card => card.Suit)
                                    .Select(cards => new
                                    {
                                        Sum = cards.Sum(card => card.Value)
                                    })
                                    .GroupBy(card => card.Sum)
                                    .Cast<int>()
                                    .ToList()
                                    .Max();


                return new KeyValuePair<List<ICard>, int>(cards, bestValue);
            }

            public List<ICard> GetCards()
            {
                return this.cards;
            }
        }

    }

    public interface ICombinationChecker {

        /// <summary>
        /// return true if the cards have the same name value
        /// <summary>
        static bool IsTris(List<ICard> cards)
        {
            foreach (ICard card in cards)
            {
                if (!cards[0].Name.Equals(card.Name))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Return true if the cards is have the same suit and are consecutive
        /// </summary>
        static bool IsFlush(List<ICard> cards)
        {
            if (!AreSameSuit(cards))
            {
                return false;
            }

            if (IsAceLow(cards))
            {
                return true;
            }

            List<int> list = cards.Select(card => GetCardIntegerValue(card))
                                  .Cast<int>()
                                  .ToList();

            int max = list.Max();
            int min = list.Min();

            return (max - min) == 2;
        }

        /// <summary>
        /// Simply if not all Card have the same value as Suit
        /// <summary>
        static bool AreSameSuit(List<ICard> cards)
        {
            foreach (Card card in cards)
            {
                if (!cards[0].Suit.Equals(card.Suit))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// return true if the Ace card is in combination with 2 and 3, obviously it has to have the same suit
        /// <summary>
        static bool IsAceLow(List<ICard> cards)
        {
            Name[] lowDeck = { Name.Asso, Name.Due, Name.Tre };

            return AreSameSuit(cards) && cards.Select(card => card.Name)
                                              .ToList()
                                              .All(new List<Name>(lowDeck).Contains);
        }

        static int GetCardIntegerValue(ICard card)
        {
            if (card.Name.Equals(Name.Fante))
            {
                return 8;
            }

            if (card.Name.Equals(Name.Cavallo))
            {
                return 9;
            }

            return card.Value;
        }

        static bool IsAnyKindOfPetrangola(List<ICard> cardList)
        {
            return IsFlush(cardList) || IsTris(cardList);
        }
    }
}
