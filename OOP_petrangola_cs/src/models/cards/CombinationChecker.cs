using System.Collections.Generic;
using System.Linq;

namespace OOP_petrangola_cs.models.cards
{
    public interface ICombinationChecker
    {

        /// <summary>
        /// return true if the cards have the same name value
        /// </summary>
        static bool IsTris(List<ICard> cards)
        {
            return cards.All(card => cards[0].Name.Equals(card.Name));
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

            var list = cards.Select(GetCardIntegerValue).ToList();
            var max = list.Max();
            var min = list.Min();

            return (max - min) == 2;
        }

        /// <summary>
        /// Simply if not all Card have the same value as Suit
        /// </summary>
        static bool AreSameSuit(List<ICard> cards)
        {
            foreach (var card1 in cards)
            {
                var card = (Card) card1;
                if (!cards[0].Suit.Equals(card.Suit))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// return true if the Ace card is in combination with 2 and 3, obviously it has to have the same suit
        /// </summary>
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

            return card.Name.Equals(Name.Cavallo) ? 9 : card.Value;
        }

        static bool IsAnyKindOfPetrangola(List<ICard> cardList)
        {
            return IsFlush(cardList) || IsTris(cardList);
        }
    }
}
