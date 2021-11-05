using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_petrangola_cs.models.cards
{
    class CombinationBuilder : ICombinationBuilder
    {
        private List<ICard> cards;

        public ICombination Build()
        {
            return new Combination(this);
        }

        public ICombinationBuilder SetCards(List<ICard> cards)
        {
            if (cards.Count == 3)
            {
                this.cards = cards;
            }
            else
            {
                throw new System.Exception("Too many cards");
            }

            return this;
        }

        public List<ICard> GetCards()
        {
            return cards;
        }

        private class Combination : ICombination
        {
            private readonly ICombinationBuilder builder;
            private readonly List<IObserver<ICombination>> observers = new List<IObserver<ICombination>>();

            private List<ICard> cards;

            public Combination(ICombinationBuilder builder)
            {
                this.builder = builder;
                cards = builder.GetCards();
            }

            public KeyValuePair<List<ICard>, int> GetBest()
            {
                List<ICard> cards = new List<ICard>(GetCards());

                if (ICombinationChecker.IsTris(cards))
                {
                    return new KeyValuePair<List<ICard>, int>(cards, cards[0].Value * 3);
                }

                if (ICombinationChecker.IsAceLow(cards))
                {
                    cards = cards.Where(card => card.Name.Equals(Name.Asso))
                                 .Select(card => new AceLow(card.Name, card.Suit))
                                 .Cast<ICard>()
                                 .ToList();
                }

                int bestValue = cards.GroupBy(card => card.Suit)
                                    .Select(cards => new
                                    {
                                        Sum = cards.Sum(card => card.Value)
                                    })
                                    .GroupBy(card => card.Sum)
                                    .Cast<int>()
                                    .ToList()
                                    .Max();


                return new KeyValuePair<List<ICard>, int>(cards, bestValue);
            }

            public List<ICard> GetCards()
            {
                return cards;
            }

            public void ReplaceCards(List<ICard> cardsToPut, List<ICard> cardsToReplace)
            {
                List<ICard> tempCards = new List<ICard>(GetCards());

                foreach (ICard cardToReplace in cardsToReplace)
                {
                    tempCards.Remove(cardToReplace);
                }
                
                foreach (ICard cardToPut in cardsToPut)
                {
                    tempCards.Add(cardToPut);
                }

                SetCards(tempCards);
                Notify(this);
            }

            private void SetCards(List<ICard> cards) => this.cards = cards;

            public IDisposable Subscribe(IObserver<ICombination> observer)
            {
                if (!observers.Contains(observer))
                    observers.Add(observer);

                return new Unsubscriber(observers, observer);
            }

            private void Notify(ICombination combination)
            {
                foreach(var o in observers)
                {
                    o.OnNext(combination);
                }
            }
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/standard/events/how-to-implement-a-provider
        /// </summary>
        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<ICombination>> _observers;
            private IObserver<ICombination> _observer;

            public Unsubscriber(List<IObserver<ICombination>> observers, IObserver<ICombination> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);
            }
        }
    }

    
}
