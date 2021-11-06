using System;
using System.Collections.Generic;

namespace OOP_petrangola_cs.models.cards
{
    public class CardFactory : ICardFactory
    {
        public List<ICard> CreateDeck()
        {
            return ShuffleCards(CreateSimpleDeck());
        }

        private static List<ICard> ShuffleCards(List<ICard> deck)
        {
            Shuffle(deck);

            return deck;
        }

        private List<ICard> CreateSimpleDeck()
        {
            List<ICard> deck = new List<ICard>();

            foreach(Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Name name in Enum.GetValues(typeof(Name)))
                {
                    deck.Add(new Card(name, suit));
                }
            }

            return deck;
        }


        /// <summary>
        ///  Thanks to https://stackoverflow.com/questions/273313/randomize-a-listt
        /// </summary>
        private static void Shuffle<T>(List<T> list)
        {
            var random = new Random();
            var n = list.Count;

            while (n > 1)
            {
                n--;
                
                var k = random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}