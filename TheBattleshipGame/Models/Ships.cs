namespace TheBattleshipGame.Models;

public record Carrier(): Ship(5);
public record Battleship(): Ship(4);
public record Cruiser() : Ship(3);
public record Submarine() : Ship(3);
public record Destroyer() : Ship(2);
public abstract record Ship(int Size)
{
    public int Hits { get; set; }
    public bool Destroyed => Hits == Size;
}
