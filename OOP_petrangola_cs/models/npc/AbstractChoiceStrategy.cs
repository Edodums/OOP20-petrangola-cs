using OOP_petrangola_cs.models.cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_petrangola_cs.models.npc
{
    public abstract class AbstractChoiceStrategy : IChoiceStrategy
    {
        public abstract List<ICards> ChooseCards(List<ICards> cardsList);

        public ICards GetBoardCards(List<ICards> cardsList)
        {
            static bool isCommunityCards(ICards cards) => cards.Community;
            return GetCardsByPredicate(cardsList, isCommunityCards);
        
        }

        public ICards GetPlayerCards(List<ICards> cardsList)
        {
            static bool isPlayerCards(ICards cards) => !cards.Community;
            return GetCardsByPredicate(cardsList, isPlayerCards);
        }

        private ICards GetCardsByPredicate(List<ICards> cardsList, Predicate<ICards> predicate)
        {
            return cardsList.Where(cards => predicate(cards)).First();
        }
    }
}
