using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_petrangola_cs.models
{
    public interface IChoiceStrategy
    {
        /// <summary>
        /// cardList has inside a player cards model and the board cards model
        /// the method returns return the two model updated after applying the choosing strategy
        /// <summary>
        List<ICards> ChooseCards(List<ICards> cardsList);
    }
}
