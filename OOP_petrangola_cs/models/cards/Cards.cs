namespace OOP_petrangola_cs.models.cards
{
    public class Cards : ICards
    {
        public Cards(ICombination combination, bool community)
        {
            Combination = combination;
            Community = community;
        }

        public ICombination Combination { get; }
        public bool Community { get; }
    }
}
