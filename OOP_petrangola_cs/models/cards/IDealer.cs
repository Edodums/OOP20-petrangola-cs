using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_petrangola_cs.models.cards
{
    public interface IDealer
    {
        List<ICards> GetCardsToDeal(int playerDetailsSize)
        {
            ICardFactory cardFactory = new CardFactory();
            ICardsFactory cardsFactory = new CardsFactory();
            CombinationFactory combinationFactory = new CombinationFactory();
            List<ICombination> combinations = combinationFactory.CreateCombinations(cardFactory.CreateDeck(), playerDetailsSize);

            for (int index = 0; index < playerDetailsSize; index++)
            {
                cardsToDeal.put(index, combinations[index]);
            }

            cardsToDeal.put(board, combinations[combinations.Count - 1]);


            return cardsFactory.createCards(cardsToDeal);
        }
    }
}
