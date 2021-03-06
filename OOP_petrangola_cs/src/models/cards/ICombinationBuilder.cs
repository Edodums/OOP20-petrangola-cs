using System.Collections.Generic;

namespace OOP_petrangola_cs.models.cards
{
    public interface ICombinationBuilder
    {  
        ICombinationBuilder SetCards(List<ICard> cardsList);

        ICombination Build();

        List<ICard> GetCards();
    }
}
