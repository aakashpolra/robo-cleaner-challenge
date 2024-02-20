using RoboCleaner.Enums;
using RoboCleaner.Models;

namespace RoboCleaner.Services;

public class HouseCleaningService
{
    private readonly Cell[][] _grid;
    private const Direction StartingDirection = Direction.Right;
    private PositionVector2D _startingPosition;

    public HouseCleaningService(string[] input)
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
                _grid[row][column] = new Cell { IsObstacle = (CellType)input[row][column] == CellType.Obstacle };
                if ((CellType)input[row][column] == CellType.StartingPoint)
                {
                    _startingPosition = new PositionVector2D(column, row, StartingDirection);
                }
            }
        }
    }

    public int CleanFloor()
    {
        PositionVector2D currentPosition = _startingPosition;
        int numCellsVisited = 1;

        while (Visit(currentPosition))
        {
            var movementResult = Move(currentPosition);
            numCellsVisited += movementResult.NumberOfUniqueCellsVisited;
            currentPosition = movementResult.NewPosition;
        }
        return numCellsVisited;
    }

    private bool Visit(PositionVector2D position)
    {
        return _grid[position.Y][position.X].Visit(position.Direction);
    }

    private MovementResult Move(PositionVector2D currentPosition)
    {
        PositionVector2D nextPosition = currentPosition.Direction switch
        {
            Direction.Left => currentPosition with { X = currentPosition.X - 1 },
            Direction.Right => currentPosition with { X = currentPosition.X + 1 },
            Direction.Up => currentPosition with { Y = currentPosition.Y - 1 },
            Direction.Down => currentPosition with { Y = currentPosition.Y + 1 }
        };

        // Rotate
        if (!IsValidPosition(nextPosition))
        {
            return new MovementResult(0, currentPosition with { Direction = Rotate(currentPosition.Direction) });
        }

        // Move
        int cellsMoved = _grid[nextPosition.Y][nextPosition.X].PreviouslyVisited() ? 0 : 1;
        return new MovementResult(cellsMoved, nextPosition);
    }

    private bool IsValidPosition(PositionVector2D position) =>
        !(
        position.Y < 0 || position.Y >= _grid.Length ||
        position.X < 0 || position.X >= _grid[position.Y].Length ||
        _grid[position.Y][position.X].IsObstacle
        );

    private Direction Rotate(Direction direction) => direction switch
    {
        Direction.Up => Direction.Left,
        Direction.Left => Direction.Down,
        Direction.Down => Direction.Right,
        Direction.Right => Direction.Up
    };
}
