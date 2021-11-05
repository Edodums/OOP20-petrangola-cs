using OOP_petrangola_cs.models.cards;
using OOP_petrangola_cs.models.npc;
using OOP_petrangola_cs.src.utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_petrangola_cs
{
    public class Program
    {
        private const int playerDetailsSize = 2;

        public static void Main()
        {
            IChoiceStrategy bestChoice = new BestChoice();
            IChoiceStrategy randomChoice = new RandomChoice();

            List<ICards> cardsList = IDealer.GetCardsToDeal(playerDetailsSize);

            ICards boardCards = cardsList.Where(cards => cards.Community).First();

            IArrayService.SplitMidPoint(cardsList.Where(cards => !cards.Community).ToArray(),
                                    out ICards[] first,
                                    out ICards[] second);


            Console.WriteLine("====== Best Choice ======");
            Console.WriteLine("Players affected: " + first.Length);
            foreach (ICards cards in first)
            {
                Console.WriteLine("Old player cards " + String.Join(", ", cards.Combination.GetCards().Select(card => card.FullName)));
                Console.WriteLine("Old board cards" + String.Join(", ", boardCards.Combination.GetCards().Select(card => card.FullName)));
                
                var listOfCards = new List<ICards>() { cards, boardCards };
                var bestChoiceExchangedCards = bestChoice.ChooseCards(listOfCards);

                boardCards = bestChoiceExchangedCards.Where(cards => cards.Community).First();

                var newPlayerCards = bestChoiceExchangedCards.Where(cards => !cards.Community).First();

                Console.WriteLine("New player cards: " +  String.Join(", ", newPlayerCards.Combination.GetCards().Select(card => card.FullName)));
                Console.WriteLine("New board cards: "  + String.Join(", ", boardCards.Combination.GetCards().Select(card => card.FullName)));
            }
            Console.WriteLine("\n");

            Console.WriteLine("====== Random Choice ======");
            Console.WriteLine("Players affected: " + second.Length);
            foreach (ICards cards in second)
            {
                Console.WriteLine("Old player cards " + String.Join(", ", cards.Combination.GetCards().Select(card => card.FullName)));
                Console.WriteLine("Old board cards" + String.Join(", ", boardCards.Combination.GetCards().Select(card => card.FullName)));

                var listOfCards = new List<ICards>() { cards, boardCards };
                var randomChoiceExchangedCards = randomChoice.ChooseCards(listOfCards);

                boardCards = randomChoiceExchangedCards.Where(cards => cards.Community).First();

                var newPlayerCards = randomChoiceExchangedCards.Where(cards => !cards.Community).First();

                Console.WriteLine("New player cards: " + String.Join(", ", newPlayerCards.Combination.GetCards().Select(card => card.FullName)));
                Console.WriteLine("New board cards: " + String.Join(", ", boardCards.Combination.GetCards().Select(card => card.FullName)));
            }
        }

    }
}
