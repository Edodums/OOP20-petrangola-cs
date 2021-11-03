using System.Collections.Generic;

namespace OOP_petrangola_cs.models
{
    public interface ICombinationFactory
    {
        List<ICombination> CreateCombinations(List<ICard> cardList, int playersSize);
    }
}
