using TheBattleshipGame.Models;

namespace TheBattleshipGame.Tests;

public class BattlefieldTests
{
    [Test]
    [TestCaseSource(nameof(AttackCoordinatesValues))]
    public void Ship_Attacking_Scenarios(AttackInput input)
    {
        // Arrange
        var startRow = 0;
        var startCol = 0;
        
        var battlefield = new Battlefield(10);
        var shipDeployer = new TestShipDeployer(battlefield);
        shipDeployer.DeploySingleShip(new Submarine(), startRow,startCol, true);
        
        // Act
        foreach (var coordinate in input.Coordinates)
        {
            battlefield.Attack(coordinate);    
        }

        // Assert
        var ship = battlefield[startRow][0].Ship;
        Assert.That(ship!.Hits == input.Hits);
        Assert.That(ship!.Destroyed == input.IsDestroyed);
        Assert.That(battlefield.AllShipsDestroyed == input.AllShipsDestroyed);
    }
    
    public record AttackInput(List<(int, int)> Coordinates, int Hits, bool IsDestroyed, bool AllShipsDestroyed);
    public static IEnumerable<AttackInput> AttackCoordinatesValues
    {
        get
        {
            yield return new AttackInput(new List<(int, int)>
                {
                    (0, 0)
                },
                1, false, false);
            
            yield return new AttackInput(new List<(int, int)>
                {
                    // submarine
                    (0, 0), (0, 1), (0, 2)
                },
                3, true, true);
        }
    }
}