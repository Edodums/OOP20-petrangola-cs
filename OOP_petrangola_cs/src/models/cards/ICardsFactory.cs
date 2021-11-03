using System.Collections.Generic;

namespace OOP_petrangola_cs.models.cards
{
    public interface ICardsFactory
    {
        List<ICards> CreateCards(List<ICombination> list);
    }
}