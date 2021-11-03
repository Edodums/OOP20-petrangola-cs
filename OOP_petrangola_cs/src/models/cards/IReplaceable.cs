using System.Collections.Generic;

namespace OOP_petrangola_cs.models.cards
{
    public interface IReplaceable
    {
        void ReplaceCards(List<ICard> cardsToPut, List<ICard> cardsToReplace);
    }
}
