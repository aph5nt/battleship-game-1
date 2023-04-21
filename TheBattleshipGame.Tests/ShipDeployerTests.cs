using TheBattleshipGame.Models;

namespace TheBattleshipGame.Tests;

public class ShipDeployerTests
{
    [Test]
    public void Should_throw_exception_when_battlefield_is_too_small()
    {
        // Arrange
        var battlefield = new Battlefield(2);
        var shipDeployer = new ShipDeployer(battlefield);

        // Act & Assert
        Assert.Throws<Exception>(() =>
        {
            shipDeployer.Deploy(new List<Ship>
            {
                new Submarine()
            });
        });
    }
    
    [Test]
    public void Should_deploy_randomly_ships_to_the_battlefield()
    {
        // Arrange
        var battlefield = new Battlefield(10);
        var shipDeployer = new ShipDeployer(battlefield);

        // Act
        shipDeployer.Deploy(new List<Ship>
        {
            new Carrier(),
            new Battleship(),
            new Cruiser(),
            new Submarine(),
            new Destroyer()
        });
        
        // Assert
        Assert.That(battlefield.Ships.Count == 5);
        
        foreach (var ship in battlefield.Ships)
        {
            Assert.That(ship.Hits == 0);    
            Assert.False(ship.Destroyed);
        }
        
    }
}