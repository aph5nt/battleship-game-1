using TheBattleshipGame.Models;

namespace TheBattleshipGame;

public class ShipDeployer
{
    private const int MaxAttempts = 1000;
    
    private readonly Battlefield _battlefield;

    public ShipDeployer(Battlefield battlefield)
    {
        _battlefield = battlefield;
    }
    
    public void Deploy(List<Ship> ships)
    {
        var rand = new Random();
        foreach (var ship in ships)
        {
            int attempts = 0;
            bool isPlaced = false;
            
            while (attempts < MaxAttempts)
            {
                var isHorizontal = rand.Next(2) == 0;
                var startRow = rand.Next(_battlefield.Size);
                var startCol = rand.Next(_battlefield.Size);
                
                if (CanDeploy(ship, startRow, startCol, isHorizontal))
                {
                    Deploy(ship, startRow, startCol, isHorizontal);
                    isPlaced = true;
                    break;
                }
                
                attempts++;
            }
            
            if (!isPlaced)
            {
                throw new Exception("Failed to deploy a ship. The battlefield is too small");
            }
        }
    }
     
    protected bool CanDeploy(Ship ship, int startRow, int startCol, bool isHorizontal)
    {
        if (isHorizontal && startCol + ship.Size > _battlefield.Size)
        {
            return false;
        }

        if (!isHorizontal && startRow + ship.Size > _battlefield.Size)
        {
            return false;
        }

        var fields = GetFieldsForShip(startRow, startCol, isHorizontal, ship.Size);
        
        return fields.All(field => IsFieldAllowed(field.row, field.col));
    }

    protected void Deploy(Ship ship, int startRow, int startCol, bool isHorizontal)
    {
        var fields = GetFieldsForShip(startRow, startCol, isHorizontal, ship.Size).ToArray();
        
        foreach (var field in fields)
        {
            _battlefield.Fields[field.row][field.col].Ship = ship;
        }

        _battlefield.Ships.Add(ship);
    }

    private bool IsFieldAllowed(int row, int col)
    {
        bool result = _battlefield.Fields[row][col].Ship == null;

        bool TryGetField(int row, int col)
        {
            var minRowIndex = 0;
            var maxRowIndex = _battlefield.Size - 1;
            
            var minColIndex = 0;
            var maxColIndex = _battlefield.Size - 1;;

            if ((row > minRowIndex && row < maxRowIndex) && (col > minColIndex && col < maxColIndex))
            {
                return _battlefield.Fields[row][col].Ship == null;
            }

            return true;
        }
        
        return result &&
               TryGetField(row - 1, col) &&
               TryGetField(row + 1, col) &&
               TryGetField(row, col + 1) &&
               TryGetField(row, col - 1)
            ;
    }

    private IEnumerable<(int row, int col)> GetFieldsForShip(int startRow, int startCol, bool isHorizontal, int length)
    {
        for (var i = 0; i < length; i++)
        {
            yield return isHorizontal ? (startRow, startCol + i) : (startRow + i, startCol);
        }
    }
}