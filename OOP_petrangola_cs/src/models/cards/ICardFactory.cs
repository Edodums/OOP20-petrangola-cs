using System.Collections.Generic;

namespace OOP_petrangola_cs.models.cards
{
    public interface ICardFactory
    {
        List<ICard> CreateDeck();
    }
}