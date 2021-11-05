using System;

namespace OOP_petrangola_cs.models.cards
{
    public class Cards : ICards, IObserver<ICombination>
    {
        public Cards(ICombination combination, bool community)
        {
            Combination = combination;
            combination.Subscribe(this);

            Community = community;
        }

        public ICombination Combination { get; private set; }
        public bool Community { get; }

        public void OnCompleted()
        {
            Console.WriteLine("Combination changed");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error);
        }

        public void OnNext(ICombination value)
        {
            Combination = value;
        }
    }
}
