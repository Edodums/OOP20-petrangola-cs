using System.Collections.Generic;

namespace OOP_petrangola_cs.models
{
    public interface ICombination
    {
        KeyValuePair<List<ICard>, int> GetBest();

        List<ICard> GetCards();
    }
}