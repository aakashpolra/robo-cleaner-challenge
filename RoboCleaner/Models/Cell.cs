using RoboCleaner.Enums;

namespace RoboCleaner.Models;

/// <summary>
/// Represents a cell or an area that can be visited in multiple directions.
/// </summary>
public class Cell()
{
    private readonly HashSet<Direction> visitedDirections = new();

    /// <summary>
    /// Whether this cell is of type Obstacle.
    /// </summary>
    /// <see cref="CellType.Obstacle"/>
    public bool IsObstacle { get; init; }

    /// <summary>
    /// Visits the cell and records the direction it is being visited with.
    /// </summary>
    /// <param name="direction">The direction of the visit.</param>
    /// <returns>False if the cell has been previously visited in the same direction.</returns>
    public bool Visit(Direction direction)
    {
        if (visitedDirections.Contains(direction))
        {
            return false;
        }
        visitedDirections.Add(direction);
        return true;
    }

    /// <summary>
    /// Whether the cell has been previously visited at all.
    /// </summary>
    /// <returns>True if the cell has been previously visited in any direction.</returns>
    public bool PreviouslyVisited() => visitedDirections.Any();
}
