using System.Collections.Generic;

namespace OOP_petrangola_cs.models.cards
{
    class CombinationFactory : ICombinationFactory
    {
        public List<ICombination> CreateCombinations(List<ICard> cardList, int playersSize)
        {
            List<ICombination> combinations = new List<ICombination>();

            int deckSize = 3;
            int length = cardList.Count - ((playersSize + 1) * deckSize);

            for (int i = cardList.Count - 1; i > length; i -= deckSize)
            {
                ICard card1 = cardList[i];
                ICard card2 = cardList[i - 1];
                ICard card3 = cardList[i - 2];

                ICard[] deck = { card1, card2, card3 };

                combinations.Add(new CombinationBuilder().SetCards(new List<ICard>(deck)).Build());
            }

            return combinations;
        }
    }
}
