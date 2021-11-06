using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_petrangola_cs.models.cards
{
    internal class CombinationBuilder : ICombinationBuilder
    {
        private List<ICard> cards;

        public ICombination Build()
        {
            return new Combination(this);
        }

        public ICombinationBuilder SetCards(List<ICard> cardsList)
        {
            if (cardsList.Count == 3)
            {
                this.cards = cardsList;
            }
            else
            {
                throw new Exception("Too many cards");
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
                List<ICard> cardList = new List<ICard>(GetCards());

                if (ICombinationChecker.IsTris(cardList))
                {
                    return new KeyValuePair<List<ICard>, int>(cardList, cardList[0].Value * 3);
                }

                if (ICombinationChecker.IsAceLow(cardList))
                {
                    cardList = cardList.Where(card => card.Name.Equals(Name.Asso))
                                 .Select(card => new AceLow(card.Name, card.Suit))
                                 .Cast<ICard>()
                                 .ToList();
                }

                
                int bestValue = cardList.GroupBy(card => card.Suit)
                                     .Select(cardListSuited => new
                                     {
                                         Sum = cardListSuited.Sum(card => card.Value)
                                     })
                                     .Max(card => card.Sum);

                return new KeyValuePair<List<ICard>, int>(cardList, bestValue);
            }

            public List<ICard> GetCards()
            {
                return cards;
            }

            public void ReplaceCards(List<ICard> cardsToPut, List<ICard> cardsToReplace)
            {
                var tempCards = new List<ICard>(GetCards());

                foreach (var cardToReplace in cardsToReplace)
                {
                    tempCards.Remove(cardToReplace);
                }

                tempCards.AddRange(cardsToPut);

                SetCards(tempCards);
                Notify(this);
            }

            private void SetCards(List<ICard> cardsList) => this.cards = cardsList;

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
            private readonly List<IObserver<ICombination>> observers;
            private readonly IObserver<ICombination> observer;

            public Unsubscriber(List<IObserver<ICombination>> observers, IObserver<ICombination> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            public void Dispose()
            {
                if (observer != null) observers.Remove(observer);
            }
        }
    }

    
}
