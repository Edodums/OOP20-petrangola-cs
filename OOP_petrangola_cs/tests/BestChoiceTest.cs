using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OOP_petrangola_cs.models.cards;
using OOP_petrangola_cs.models.npc;

namespace OOP_petrangola_cs.tests
{
    class BestChoiceTest
    {
        private readonly int playerDetailsSize = 2;
        private IChoiceStrategy bestChoice;
        private List<ICards> cardsList;
        private List<ICards> playersCardsList;
        private ICards boardCards;

        [SetUp]
        public void Setup()
        {
            bestChoice = new BestChoice();
            cardsList = IDealer.GetCardsToDeal(playerDetailsSize);

            playersCardsList = cardsList.Where(cards => !cards.Community).ToList();
            boardCards = cardsList.First(cards => cards.Community);
        }

        [Test]
        public void IsActuallyTheBestChoiceTest()
        {
            foreach(ICards oldPlayerCards in playersCardsList)
            {
                var listOfCards = new List<ICards>() { oldPlayerCards, boardCards };
                var bestChoiceExchangedCards = bestChoice.ChooseCards(listOfCards);
                
                boardCards = bestChoiceExchangedCards.First(cards => cards.Community);

                var newPlayerCards = bestChoiceExchangedCards.First(cards => !cards.Community);

                var cardList = listOfCards.SelectMany(cards => cards.Combination.GetCards()).ToList();
                var maxCombination = GetMaxCombinationListOfCards(cardList);

                var newPlayerListOfCards = newPlayerCards.Combination.GetCards();

                Assert.AreEqual(maxCombination, newPlayerCards.Combination.GetCards());

                foreach(var card in newPlayerListOfCards)
                {
                    Assert.True(maxCombination.Contains(card));
                }

                var newCardsPair = newPlayerCards.Combination.GetBest();
                var oldCardsPair = oldPlayerCards.Combination.GetBest();

                var newCardsValue = newCardsPair.Value;
                var oldCardsValue = oldCardsPair.Value;

                Assert.GreaterOrEqual(newCardsValue, oldCardsValue);
            }
        }

        private List<ICard> GetMaxCombinationListOfCards(List<ICard> cardList)
        {
            var combinations = GenerateAllCombinations(cardList);

            var tris = combinations.Where(ICombinationChecker.IsTris);

            if (tris.Any())
            {
                return tris.First();
            }

            var flush = combinations.Where(ICombinationChecker.IsFlush);

            if (flush.Any())
            {
                return flush.First();
            }

            var flushWithAceLow = combinations.Where(ICombinationChecker.IsAceLow);

            if (flushWithAceLow.Any())
            {
                return flushWithAceLow.First();
            }

            return combinations
                  .Select(cards => new KeyValuePair<List<ICard>, int>(cards, GetMaxCombination(cards)))
                  .OrderByDescending(pair => pair.Value)
                  .First()
                  .Key;
        }

        private List<List<ICard>> GenerateAllCombinations(List<ICard> cardList)
        {
            var permutations = GetPermutations(cardList, 3);

            return permutations.Select(listOfCards => listOfCards.ToList()).ToList();
        }

        /// <summary>
        /// Thanks to https://stackoverflow.com/a/10629938/13455322 : Permutations without repetition using recursion
        /// </summary>
        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            return length == 1 ? list.Select(t => new [] { t })
                               : GetPermutations(list, length - 1).SelectMany(t => list.Where(obj => !t.Contains(obj)), (t1, t2) => t1.Concat(new [] { t2 }));
        }


        private static int GetMaxCombination(List<ICard> cardList)
        {
            return cardList.GroupBy(card => card.Suit)
                           .Select(cardEntry => new KeyValuePair<Suit, int>(cardEntry.Key, cardEntry.Sum(card => card.Value)))
                           .Max(card => card.Value);
        }
    }
}