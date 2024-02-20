using RoboCleaner.Enums;
using RoboCleaner.Models;

namespace RoboCleaner.Services;

public class HouseCleaningService(Grid2D grid)
{
    public int CleanFloor()
    {
        PositionVector2D currentPosition = grid.StartingPosition;
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
        return grid.Cells[position.Y][position.X].Visit(position.Direction);
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
        int cellsMoved = grid.Cells[nextPosition.Y][nextPosition.X].PreviouslyVisited() ? 0 : 1;
        return new MovementResult(cellsMoved, nextPosition);
    }

    private bool IsValidPosition(PositionVector2D position) =>
        !(
        position.Y < 0 || position.Y >= grid.Cells.Length ||
        position.X < 0 || position.X >= grid.Cells[position.Y].Length ||
        grid.Cells[position.Y][position.X].IsObstacle
        );

    private Direction Rotate(Direction direction) => direction switch
    {
        Direction.Up => Direction.Left,
        Direction.Left => Direction.Down,
        Direction.Down => Direction.Right,
        Direction.Right => Direction.Up
    };
}
