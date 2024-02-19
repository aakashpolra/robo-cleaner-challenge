namespace RoboCleaner;

public record Position(int Row, int Column);
public enum Direction { Right = 0, Up = 1, Left = 2, Down = 3 }

public class Constants
{
    public const char EmptyCell = '.';
    public const char Obstacle = 'o';
    public const char StartingPoint = 's';
}

public class Cell()
{
    private readonly Dictionary<Direction, bool> visited = new();
    public bool IsObstacle { get; init; }

    public bool Visit(Direction direction)
    {
        if (!visited.ContainsKey(direction))
        {
            visited[direction] = true;
            return true;
        }
        return false;
    }
}

public class HouseCleaningRobot
{
    private readonly Cell[][] grid;

    public HouseCleaningRobot(string[] input)
    {
        grid = new Cell[input.Length][];
        InitializeGrid(input);
    }

    private void InitializeGrid(string[] input)
    {
        for (int row = 0; row < grid.Length; row++)
        {
            for (int column = 0; column < grid[row].Length; column++)
            {
                grid[row][column] = new Cell { IsObstacle = input[row][column] == Constants.Obstacle };
            }
        }
    }
}
