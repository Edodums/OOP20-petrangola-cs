using OOP_petrangola_cs.models.cards;
using System.Collections.Generic;
using System.Linq;

namespace OOP_petrangola_cs.models.npc
{
    public class BestChoice : AbstractChoiceStrategy
    {
        public override List<ICards> ChooseCards(List<ICards> cardsList)
        {
            var cardList = cardsList.SelectMany(cards => cards.Combination.GetCards()).ToList();

            var boardCards = GetBoardCards(cardsList);
            var playerCards = GetPlayerCards(cardsList);

            var maxCombination = GetMaxCombinationListOfCards(cardList);
            var complement = cardList.Where(card => !maxCombination.Contains(card)).ToList();

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
            var combinations = new List<List<ICard>>();
            var permutations  = GetPermutations(cardList, 3);

            foreach (IEnumerable<ICard> listOfCards in permutations)
            {
                combinations.Add(listOfCards.ToList());
            }
            
            return combinations;
        }

        /// <summary>
        /// Thanks to https://stackoverflow.com/a/10629938/13455322 : Permutations without repetition using recursion
        /// </summary>
        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            return length == 1 ? list.Select(t => new[] { t })
                               : GetPermutations(list, length - 1).SelectMany(t => list.Where(obj => !t.Contains(obj)), (t1, t2) => t1.Concat(new[] { t2 }));
        }


        private static int GetMaxCombination(List<ICard> cardList)
        {
            return cardList.GroupBy(card => card.Suit)
                           .Select(cardEntry => new KeyValuePair<Suit, int>(cardEntry.Key, cardEntry.Sum(card => card.Value)))
                           .Max(card => card.Value);
        }
    }
   
}
