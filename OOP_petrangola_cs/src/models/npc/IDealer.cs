using OOP_petrangola_cs.models.cards;
using System.Collections.Generic;

namespace OOP_petrangola_cs.models.npc
{
    public interface IDealer
    {
        static List<ICards> GetCardsToDeal(int playerDetailsSize)
        {
            ICardFactory cardFactory = new CardFactory();
            ICardsFactory cardsFactory = new CardsFactory();
            var combinationFactory = new CombinationFactory();
            var cardsToDeal = new List<ICombination>();
            var combinations = combinationFactory.CreateCombinations(cardFactory.CreateDeck(), playerDetailsSize);

            for (var index = 0; index < playerDetailsSize; index++)
            {
                cardsToDeal.Add(combinations[index]);
            }

            cardsToDeal.Add(combinations[^1]);

            return cardsFactory.CreateCards(cardsToDeal);
        }
    }
}
