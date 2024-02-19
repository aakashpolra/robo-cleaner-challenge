namespace RoboCleaner;

public enum Direction { Right = 0, Up = 1, Left = 2, Down = 3 }
public record Position(int Row, int Column, Direction Direction);

public record MovementResult (int NumCellsMoved, Position NewPosition);

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
    private const Direction StartingDirection = Direction.Right;
    private Position _startingPosition;

    public HouseCleaningRobot(string[] input)
    {
        _grid = new Cell[input.Length][];
        InitializeGrid(input);
    }

    private void InitializeGrid(string[] input)
    {
        for (int row = 0; row < _grid.Length; row++)
        {
            _grid[row] = new Cell[input[row].Length];
            for (int column = 0; column < _grid[row].Length; column++)
            {
                _grid[row][column] = new Cell { IsObstacle = input[row][column] == Constants.Obstacle };
                if (input[row][column] == Constants.StartingPoint)
                {
                    _startingPosition = new Position(row, column, StartingDirection);
                }
            }
        }
    }

    public int CleanFloor()
    {
        Position currentPosition = _startingPosition;
        int numCellsMoved = 0;
        
        while(Visit(currentPosition))
        {
            var movementResult = Move(currentPosition);
            numCellsMoved += movementResult.NumCellsMoved;
            currentPosition = movementResult.NewPosition;
        }
        return numCellsMoved;
    }

    private bool Visit(Position position)
    {
        return _grid[position.Row][position.Column].Visit(position.Direction);
    }

    private MovementResult Move(Position currentPosition)
    {
        Position nextPosition = currentPosition.Direction switch
        {
            Direction.Left => currentPosition with { Column = currentPosition.Column - 1 },
            Direction.Right => currentPosition with { Column = currentPosition.Column + 1 },
            Direction.Up => currentPosition with { Row = currentPosition.Row - 1 },
            Direction.Down => currentPosition with { Row = currentPosition.Row + 1 }
        };
        if (!IsValidPosition(nextPosition))
        {
            return new MovementResult(0, currentPosition with { Direction = Rotate(currentPosition.Direction) });
        }
        return new MovementResult(1, nextPosition);
    }

    private bool IsValidPosition(Position position) =>
        !(
        position.Row < 0 || position.Row >= _grid.Length ||
        position.Column < 0 || position.Column >= _grid.Length ||
        _grid[position.Row][position.Column].IsObstacle
        );

    private Direction Rotate(Direction direction) => direction switch
    {
        Direction.Up => Direction.Left,
        Direction.Left => Direction.Down,
        Direction.Down => Direction.Right,
        Direction.Right => Direction.Up,
        _ => throw new ArgumentException("Unknown direction found.")
    };
}
