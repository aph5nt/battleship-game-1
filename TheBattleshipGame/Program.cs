using Spectre.Console;
using TheBattleshipGame;
using TheBattleshipGame.Models;

const int boardSize = 10;

// initialize the battlefield
var battlefield = new Battlefield(10);
var shipDeployer = new ShipDeployer(battlefield);

shipDeployer.Deploy(new()
{
    new Carrier(),
    new Battleship(),
    new Cruiser(),
    new Submarine(),
    new Destroyer()
});

// game loop
while (!battlefield.AllShipsDestroyed)
{
    UiHelper.DisplayBoard(battlefield);
    UiHelper.DisplayStats(battlefield);
    
    var target = UiHelper.GetAttackCoordinates(boardSize);
    battlefield.Attack(target);
}

UiHelper.DisplayBoard(battlefield);

AnsiConsole.WriteLine("You won!");

Console.ReadKey();