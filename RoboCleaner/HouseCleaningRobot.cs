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
    private readonly Cell[][] _grid;
    private Position _startingPosition;
    private const Direction startingDirection = Direction.Right;

    public HouseCleaningRobot(string[] input)
    {
        _grid = new Cell[input.Length][];
        InitializeGrid(input);
    }

    private void InitializeGrid(string[] input)
    {
        for (int row = 0; row < _grid.Length; row++)
        {
            for (int column = 0; column < _grid[row].Length; column++)
            {
                _grid[row][column] = new Cell { IsObstacle = input[row][column] == Constants.Obstacle };
                if (input[row][column] == Constants.StartingPoint)
                {
                    _startingPosition = new Position(row, column);
                }
            }
        }
    }
}
