using NUnit.Framework;
using OOP_petrangola_cs.models.cards;
using OOP_petrangola_cs.models.npc;
using System.Collections.Generic;
using System.Linq;

namespace OOP_petrangola_cs.tests
{
    class RandomChoiceTest
    {
        private readonly int playerDetailsSize = 2;
        private IChoiceStrategy randomChoice;
        private List<ICards> cardsList;
        private List<ICards> playersCardsList;
        private ICards boardCards;

        [SetUp]
        public void Setup()
        {
            randomChoice = new RandomChoice();
            cardsList = IDealer.GetCardsToDeal(playerDetailsSize);

            playersCardsList = cardsList.Where(cards => !cards.Community).ToList();
            boardCards = cardsList.First(cards => cards.Community);
        }

        [Test]
        public void ActuallyWorksTest()
        {
            foreach(var cards in playersCardsList)
            {
                var listOfCards = new List<ICards>() { cards, boardCards };
                var randomChoiceExchangedCards = randomChoice.ChooseCards(listOfCards);

                if (!randomChoiceExchangedCards.Any())
                {
                    Assert.That(true, "nothing changed");
                    continue;
                }
                
                var newBoardCards = randomChoiceExchangedCards.First(cardsListTemp => cardsListTemp.Community);
                var newPlayerCards = randomChoiceExchangedCards.First(cardsListTemp => !cardsListTemp.Community);

                foreach (var unused in cards.Combination.GetCards().Where(card => newPlayerCards.Combination.GetCards().Count == 0 || !newPlayerCards.Combination.GetCards().Contains(card)))
                {
                    Assert.Pass();
                }

                foreach (var unused in boardCards.Combination.GetCards().Where(card => !newBoardCards.Combination.GetCards().Contains(card)))
                {
                    Assert.Pass();
                }
            }
        }
    }
}
