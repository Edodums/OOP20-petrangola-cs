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

        private List<ICard> ShuffleCards(List<ICard> deck)
        {
            List<ICard> tempDeck = deck;

            Shuffle(tempDeck);

            return tempDeck;
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
        private static void Shuffle<ICard>(List<ICard> list)
        {
            Random random = new Random();
            int n = list.Count;

            while (n > 1)
            {
                n--;
                
                int k = random.Next(n + 1);
                ICard value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}