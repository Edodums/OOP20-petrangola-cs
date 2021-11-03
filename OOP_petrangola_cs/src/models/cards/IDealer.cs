using System.Collections.Generic;

namespace OOP_petrangola_cs.models.cards
{
    public interface IDealer
    {
        List<ICards> GetCardsToDeal(int playerDetailsSize)
        {
            ICardFactory cardFactory = new CardFactory();
            ICardsFactory cardsFactory = new CardsFactory();
            CombinationFactory combinationFactory = new CombinationFactory();
            List<ICombination> cardsToDeal = new List<ICombination>();
            List<ICombination> combinations = combinationFactory.CreateCombinations(cardFactory.CreateDeck(), playerDetailsSize);

            for (int index = 0; index < playerDetailsSize; index++)
            {
                cardsToDeal.Add(combinations[index]);
            }

            cardsToDeal.Add(combinations[combinations.Count - 1]);

            return cardsFactory.CreateCards(cardsToDeal);
        }
    }
}
