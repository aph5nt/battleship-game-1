using TheBattleshipGame.Models;

namespace TheBattleshipGame.Tests;

public class TestShipDeployer : ShipDeployer
{
    public TestShipDeployer(Battlefield battlefield) : base(battlefield)
    {
    }

    public void DeploySingleShip(Ship ship, int startRow, int startCol, bool isHorizontal)
    {
        if (CanDeploy(ship, startRow, startCol, isHorizontal))
        {
            Deploy(ship, startRow, startCol, isHorizontal);
        }
        else
        {
            throw new Exception("Unable to deploy");
        }
    }
}