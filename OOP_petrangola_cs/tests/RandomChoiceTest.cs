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
            boardCards = cardsList.Where(cards => cards.Community).First();
        }

        [Test]
        public void ActuallyWorksTest()
        {
            foreach(ICards cards in playersCardsList)
            {
                var listOfCards = new List<ICards>() { cards, boardCards };
                var randomChoiceExchangedCards = randomChoice.ChooseCards(listOfCards);
                
                var newBoardCards = randomChoiceExchangedCards.Where(cards => cards.Community).First();
                var newPlayerCards = randomChoiceExchangedCards.Where(cards => !cards.Community).First();

                foreach(ICard card in cards.Combination.GetCards()) {
                    if (newPlayerCards.Combination.GetCards().Count == 0 || !newPlayerCards.Combination.GetCards().Contains(card))
                    {
                        Assert.Pass();
                    }
                }

                foreach (ICard card in boardCards.Combination.GetCards())
                {
                    if (!newBoardCards.Combination.GetCards().Contains(card))
                    {
                        Assert.Pass();
                    }
                }
            }
        }
    }
}
