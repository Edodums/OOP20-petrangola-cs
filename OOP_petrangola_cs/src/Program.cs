using OOP_petrangola_cs.models.cards;
using OOP_petrangola_cs.models.npc;
using System;
using System.Collections.Generic;
using System.Linq;
using OOP_petrangola_cs.utils;

namespace OOP_petrangola_cs
{
    public class Program
    {
        private const int PlayerDetailsSize = 2;

        public static void Main()
        {
            IChoiceStrategy bestChoice = new BestChoice();
            IChoiceStrategy randomChoice = new RandomChoice();

            var cardsList = IDealer.GetCardsToDeal(PlayerDetailsSize);

            var boardCards = cardsList.First(cards => cards.Community);

            IArrayService.SplitMidPoint(cardsList.Where(cards => !cards.Community).ToArray(),
                                    out var first,
                                    out var second);


            Console.WriteLine("====== Best Choice ======");
            Console.WriteLine("Players affected: " + first.Length);
            foreach (var cards in first)
            {
                Console.WriteLine("Old player cards " + string.Join(", ", cards.Combination.GetCards().Select(card => card.FullName)));
                Console.WriteLine("Old board cards" + string.Join(", ", boardCards.Combination.GetCards().Select(card => card.FullName)));
                
                var listOfCards = new List<ICards>() { cards, boardCards };
                var bestChoiceExchangedCards = bestChoice.ChooseCards(listOfCards);

                if (!bestChoiceExchangedCards.Any())
                {
                    Console.WriteLine("Old playerCards didn't change");
                    continue;
                }

                boardCards = bestChoiceExchangedCards.First(cardsLists => cardsLists.Community);

                var newPlayerCards = bestChoiceExchangedCards.First(cardsLists => !cardsLists.Community);

                Console.WriteLine("New player cards: " +  string.Join(", ", newPlayerCards.Combination.GetCards().Select(card => card.FullName)));
                Console.WriteLine("New board cards: "  + string.Join(", ", boardCards.Combination.GetCards().Select(card => card.FullName)));
            }
            Console.WriteLine("\n");


            Console.WriteLine("====== Random Choice ======");
            Console.WriteLine("Players affected: " + second.Length);
            foreach (var cards in second)
            {
                Console.WriteLine("Old player cards " + string.Join(", ", cards.Combination.GetCards().Select(card => card.FullName)));
                Console.WriteLine("Old board cards" + string.Join(", ", boardCards.Combination.GetCards().Select(card => card.FullName)));

                var listOfCards = new List<ICards> { cards, boardCards };
                var randomChoiceExchangedCards = randomChoice.ChooseCards(listOfCards);

                if (!randomChoiceExchangedCards.Any())
                {
                    Console.WriteLine("Old playerCards didn't change");
                    continue;
                }

                boardCards = randomChoiceExchangedCards.First(cardsLists => cardsLists.Community);

                var newPlayerCards = randomChoiceExchangedCards.First(cardsLists => !cardsLists.Community);

                Console.WriteLine("New player cards: " + string.Join(", ", newPlayerCards.Combination.GetCards().Select(card => card.FullName)));
                Console.WriteLine("New board cards: " + string.Join(", ", boardCards.Combination.GetCards().Select(card => card.FullName)));
            }
        }

    }
}
