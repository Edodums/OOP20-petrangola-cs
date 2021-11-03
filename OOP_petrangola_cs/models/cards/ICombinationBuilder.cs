using System.Collections.Generic;

namespace OOP_petrangola_cs.models
{
    public interface ICombinationBuilder
    {  
        ICombinationBuilder SetCards(List<ICard> cards);

        ICombination Build();

        List<ICard> GetCards();
    }
}
