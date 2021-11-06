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
            static bool IsCommunityCards(ICards cards) => cards.Community;
            return GetCardsByPredicate(cardsList, IsCommunityCards);
        
        }

        public ICards GetPlayerCards(List<ICards> cardsList)
        {
            static bool IsPlayerCards(ICards cards) => !cards.Community;
            return GetCardsByPredicate(cardsList, IsPlayerCards);
        }

        private ICards GetCardsByPredicate(List<ICards> cardsList, Predicate<ICards> predicate)
        {
            return cardsList.First(cards => predicate(cards));
        }
    }
}
