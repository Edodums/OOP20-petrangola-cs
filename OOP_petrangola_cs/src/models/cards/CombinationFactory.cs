using System.Collections.Generic;

namespace OOP_petrangola_cs.models.cards
{
    class CombinationFactory : ICombinationFactory
    {
        public List<ICombination> CreateCombinations(List<ICard> cardList, int playersSize)
        {
            var combinations = new List<ICombination>();

            const int deckSize = 3;
            var length = cardList.Count - ((playersSize + 1) * deckSize);

            for (var i = cardList.Count - 1; i > length; i -= deckSize)
            {
                var card1 = cardList[i];
                var card2 = cardList[i - 1];
                var card3 = cardList[i - 2];

                ICard[] deck = { card1, card2, card3 };

                combinations.Add(new CombinationBuilder().SetCards(new List<ICard>(deck)).Build());
            }

            return combinations;
        }
    }
}
