namespace TheBattleshipGame.Models;

public class Battlefield
{
    public readonly Field[][] Fields;
    public readonly List<Ship> Ships = new();

    public int Size { get; }
    public bool AllShipsDestroyed => Ships.All(ship => ship.Destroyed);
    public Field[] this[int row] => Fields[row];
    
    public Battlefield(int size)
    {
        Size = size;
        Fields = new Field[size][];
        
        for (var i = 0; i < Size; i++)
        {
            Fields[i] = Enumerable.Range(0, Size)
                .Select(j => new Field
                {
                    State = FieldState.Unknown
                }).ToArray();
        }
    }
 
    public void Attack((int row, int col) target)
    {
        var field = Fields[target.row][target.col];
        if (field.State != FieldState.Unknown) 
            return;

        if (field.Ship != null)
        {
            field.State = FieldState.Hit;
            field.IsHit = true;
            field.Ship.Hits++;
        }
        else
        {
            field.State = FieldState.Miss;
        }
    }
}