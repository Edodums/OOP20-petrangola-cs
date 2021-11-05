using System.Collections.Generic;
using System.Linq;

namespace OOP_petrangola_cs.models.cards
{
    public interface ICombinationChecker
    {

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
