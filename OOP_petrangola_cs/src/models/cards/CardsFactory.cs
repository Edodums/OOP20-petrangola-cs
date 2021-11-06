using System.Collections.Generic;

namespace OOP_petrangola_cs.models.cards
{
    public class CardsFactory : ICardsFactory
    {
        public List<ICards> CreateCards(List<ICombination> list)
        {
            var cardsList = new List<ICards>();

            foreach (var combination in list)
            {
                if (combination == list[^1])
                {
                    cardsList.Add(new Cards(combination, true));
                    break;
                }

                cardsList.Add(new Cards(combination, false));
            }


            return cardsList;
        }
    }
}