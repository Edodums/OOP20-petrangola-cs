using System;
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
            boardCards = cardsList.Where(cards => cards.Community).First();
        }

        [Test]
        public void IsActuallyTheBestChoiceTest()
        {
            foreach(ICards oldPlayerCards in playersCardsList)
            {
                var listOfCards = new List<ICards>() { oldPlayerCards, boardCards };
                var bestChoiceExchangedCards = bestChoice.ChooseCards(listOfCards);
                
                boardCards = bestChoiceExchangedCards.Where(cards => cards.Community).First();

                var newPlayerCards = bestChoiceExchangedCards.Where(cards => !cards.Community).First();

                List<ICard> cardList = listOfCards.SelectMany(cards => cards.Combination.GetCards()).ToList();
                List<ICard> maxCombination = GetMaxCombinationListOfCards(cardList);

                List<ICard> newPlayerListOfCards = newPlayerCards.Combination.GetCards();

                Assert.AreEqual(maxCombination, newPlayerCards.Combination.GetCards());

                foreach(ICard card in newPlayerListOfCards)
                {
                    Assert.True(maxCombination.Contains(card));
                }

                KeyValuePair<List<ICard>, int> newCardsPair = newPlayerCards.Combination.GetBest();
                KeyValuePair<List<ICard>, int> oldCardsPair = oldPlayerCards.Combination.GetBest();

                int newCardsValue = newCardsPair.Value;
                int oldCardsValue = oldCardsPair.Value;

                Assert.GreaterOrEqual(newCardsValue, oldCardsValue);
            }
        }

        private List<ICard> GetMaxCombinationListOfCards(List<ICard> cardList)
        {
            List<List<ICard>> combinations = GenerateAllCombinations(cardList);

            IEnumerable<List<ICard>> tris = combinations.Where(combination => ICombinationChecker.IsTris(combination));

            if (tris.Any())
            {
                return tris.First();
            }

            IEnumerable<List<ICard>> flush = combinations.Where(combination => ICombinationChecker.IsFlush(combination));

            if (flush.Any())
            {
                return flush.First();
            }

            IEnumerable<List<ICard>> flushWithAceLow = combinations.Where(combination => ICombinationChecker.IsAceLow(combination));

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
            List<List<ICard>> combinations = new List<List<ICard>>();
            IEnumerable<IEnumerable<ICard>> permutations = GetPermutations(cardList, 3);

            foreach (IEnumerable<ICard> listOfCards in permutations)
            {
                combinations.Add(listOfCards.ToList());
            }

            return combinations;
        }

        /// <summary>
        /// Thanks to https://stackoverflow.com/a/10629938/13455322 : Permutations without repetion using recursion
        /// </summary>
        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            return length == 1 ? list.Select(t => new T[] { t })
                               : GetPermutations(list, length - 1).SelectMany(t => list.Where(obj => !t.Contains(obj)), (t1, t2) => t1.Concat(new T[] { t2 }));
        }


        private int GetMaxCombination(List<ICard> cardList)
        {
            return cardList.GroupBy(card => card.Suit)
                           .Select(cardEntry => new KeyValuePair<Suit, int>(cardEntry.Key, cardEntry.Sum(card => card.Value)))
                           .Max(card => card.Value);
        }
    }
}