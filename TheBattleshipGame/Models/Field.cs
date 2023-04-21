namespace TheBattleshipGame.Models;

public class Field
{
    public Ship? Ship { get; set; }
    public FieldState State { get; set; }
    public bool IsHit { get; set; }

    public override string ToString()
    {
        switch (State)
        {
            case FieldState.Unknown:
                return ".";
            case FieldState.Hit:
                return "X";
            case FieldState.Miss:
                return "O";
            default:
                return string.Empty;
        }
    }
}