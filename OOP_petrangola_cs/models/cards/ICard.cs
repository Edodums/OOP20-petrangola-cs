namespace OOP_petrangola_cs.models
{
    public interface ICard
    {
        Name Name { get; }

        string FullName { get; }

        Suit Suit { get; }

        int Value { get; }
    }

    public enum Name : int {
        Asso = 11,
        Due = 2,
        Tre = 3,
        Quattro = 4,
        Cinque = 5,
        Sei = 6,
        Sette = 7,
        Fante = 10,
        Cavallo = 10,
        Re = 10
    }

    public enum Suit
    {
        Bastoni, 
        Coppe, 
        Denari, 
        Spade
    }
}