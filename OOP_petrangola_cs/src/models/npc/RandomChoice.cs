
using OOP_petrangola_cs.models.cards;
using System;
using System.Collections.Generic;

namespace OOP_petrangola_cs.models.npc
{
    public class RandomChoice : AbstractChoiceStrategy
    {
        public override List<ICards> ChooseCards(List<ICards> cardsList)
        {
            Random random = new Random();
            int indexToTake = random.Next(2);
            int indexToGive = random.Next(2);

            ICards boardCards = GetBoardCards(cardsList);
            ICards playerCards = GetPlayerCards(cardsList);

            ICard playerCard = playerCards.Combination.GetCards()[indexToGive];
            ICard boardCard = boardCards.Combination.GetCards()[indexToTake];

            if (random.NextDouble() > 0.5)
            {
                return new List<ICards>();
            }

            playerCards.Combination.ReplaceCards(new List<ICard>() { boardCard }, new List<ICard>() { playerCard });
            boardCards.Combination.ReplaceCards(new List<ICard>() { playerCard }, new List<ICard>() { boardCard });

            return new List<ICards>() { boardCards, playerCards };
        }
    }
}
