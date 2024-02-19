namespace RoboCleaner;

public enum Direction { Right = 0, Up = 1, Left = 2, Down = 3 }
public record Position(int Row, int Column, Direction Direction);

public record MovementResult (int NumCellsMoved, Position NewPosition);

public class CellTypes
{
    public const char EmptyCell = '.';
    public const char Obstacle = 'o';
    public const char StartingPoint = 's';
}

public class Cell()
{
    private readonly HashSet<Direction> visitedDirections = new();
    public bool IsObstacle { get; init; }

    public bool Visit(Direction direction)
    {
        if (visitedDirections.Contains(direction))
        {
            return false;
        }
        visitedDirections.Add(direction);
        return true;
    }

    public bool PreviouslyVisited() => visitedDirections.Any();
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
                _grid[row][column] = new Cell { IsObstacle = input[row][column] == CellTypes.Obstacle };
                if (input[row][column] == CellTypes.StartingPoint)
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

        // Rotate
        if (!IsValidPosition(nextPosition))
        {
            return new MovementResult(0, currentPosition with { Direction = Rotate(currentPosition.Direction) });
        }

        // Move
        int cellsMoved = _grid[nextPosition.Row][nextPosition.Column].PreviouslyVisited() ? 0 : 1;
        return new MovementResult(cellsMoved, nextPosition);
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
        Direction.Right => Direction.Up
    };
}
