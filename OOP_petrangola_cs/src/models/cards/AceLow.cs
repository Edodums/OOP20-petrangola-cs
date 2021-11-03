namespace OOP_petrangola_cs.models.cards
{
    public class AceLow : Card
    {
        public AceLow(Name name, Suit suit) : base(name, suit)
        {
            
        }

        public override int Value => 1;
    }
}
