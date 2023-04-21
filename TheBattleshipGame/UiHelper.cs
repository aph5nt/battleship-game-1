using Spectre.Console;
using TheBattleshipGame.Models;

namespace TheBattleshipGame;

public static class UiHelper
{
    public static void DisplayStats(Battlefield battlefield)
    {
        var table = new Table
        {
            Border = TableBorder.Square,
            ShowHeaders = true,
            Title = new TableTitle("Stats", new Style(Color.NavajoWhite1)) 
        };

        table.AddColumns("Type", "Size", "Hits", "IsDestroyed");
        
        foreach (var ship in battlefield.Ships)
        {
            table.AddRow(ship.GetType().Name, ship.Size.ToString(), ship.Hits.ToString(), ship.Destroyed.ToString());
        }
        
        AnsiConsole.Write(table);
    }
    
    public static void DisplayBoard(Battlefield battlefield)
    {
        AnsiConsole.Clear();
        
        var table = new Table
        {
            Border = TableBorder.Square,
            ShowHeaders = true,
            Title = new TableTitle("The Battleship game", new Style(Color.NavajoWhite1)) 
        };

        table.AddColumn("#");
        table.AddColumns(Enumerable.Range(0, battlefield.Size).Select(i =>  Convert.ToChar('A' + i).ToString()).ToArray());

        for (var row = 0; row < battlefield.Size; row++)
        {
            var rowData = new List<string>
            {
                (row+1).ToString()
            };
            
            rowData.AddRange(battlefield[row].Select(x => x.ToString()).ToArray());
            
            table.AddRow(rowData.ToArray());
        }

        AnsiConsole.Write(table);
    }
    
    public static (int, int) GetAttackCoordinates(int boardSize)
    {
        var column = 0;
        var row = 0;
        
        AnsiConsole.Prompt(
            new TextPrompt<string>("Enter target coordinates (e.g. A1): ")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]Invalid input. [/]")
                .Validate(input =>
                {
                    if (!char.IsLetter(input.ToUpper()[0]))
                    {
                        return ValidationResult.Error($"[red]Please enter a coordinate between A1 and {Convert.ToChar('A' + boardSize - 1)}{boardSize}: [/]");
                    }
                    
                    column = input.ToUpper()[0];
                    int.TryParse(input.Substring(1, input.Length - 1), out row);
                    if (column <= 'A' + boardSize && row >= 1 && row <= boardSize)
                    {
                        return ValidationResult.Success();
                    }

                    return ValidationResult.Error($"[red]Please enter a coordinate between A1 and {Convert.ToChar('A' + boardSize - 1)}{boardSize}: [/]");
                }));

        return (row - 1, column - 'A');
    }
}