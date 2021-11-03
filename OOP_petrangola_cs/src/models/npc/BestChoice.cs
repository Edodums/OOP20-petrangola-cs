using OOP_petrangola_cs.models.cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_petrangola_cs.models.npc
{
    public class BestChoice : AbstractChoiceStrategy
    {
        public override List<ICards> ChooseCards(List<ICards> cardsList)
        {
            List<ICard> cardList = cardsList.Select(cards => cards.Combination)
                                            .Select(combination => combination.GetCards())
                                            .Cast<ICard>()
                                            .ToList();

            ICards boardCards = GetBoardCards(cardsList);
            ICards playerCards = GetPlayerCards(cardsList);

            List<ICard> maxCombination = GetMaxCombinationListOfCards(cardList);
            List<ICard> complement = cardList.Where(card => !maxCombination.Contains(card)).ToList();

            if (maxCombination.Equals(playerCards.Combination.GetCards()))
            {
                return new List<ICards>();
            }

            playerCards.Combination.ReplaceCards(maxCombination, playerCards.Combination.GetCards());
            boardCards.Combination.ReplaceCards(complement, boardCards.Combination.GetCards());
           
            return new List<ICards>{
                playerCards,
                boardCards
            };
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
            IEnumerable<IEnumerable<ICard>> permutations  = GetPermutations(cardList, 3);

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
