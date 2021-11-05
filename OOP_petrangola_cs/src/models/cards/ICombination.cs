using System;
using System.Collections.Generic;

namespace OOP_petrangola_cs.models.cards
{
    public interface ICombination : IReplaceable, IObservable<ICombination>
    {
        KeyValuePair<List<ICard>, int> GetBest();

        List<ICard> GetCards();
    }
}