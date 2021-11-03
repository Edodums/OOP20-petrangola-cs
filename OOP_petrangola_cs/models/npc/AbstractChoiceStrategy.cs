using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_petrangola_cs.models.npc
{
    public abstract class AbstractChoiceStrategy : IChoiceStrategy
    {
        public abstract List<ICards> ChooseCards(List<ICards> cardsList);

        ICards GetBoardCards(List<ICards> cardsList)
        {
            return GetCardsByPredicate(cardsList, ICards.IsCommunity);
        
        }

        ICards GetPlayerCards(List<ICards> cardsList)
        {
            return GetCardsByPredicate(cardsList, ICards.IsPlayerCards);
        }

        private ICards GetCardsByPredicate(List<ICards> cardsList, Predicate<ICards> predicate)
        {
            return cardsList.Where(cards => predicate(cards)).First();
        }
    }
}
