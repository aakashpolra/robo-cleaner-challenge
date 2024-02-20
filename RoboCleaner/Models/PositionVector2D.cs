using RoboCleaner.Enums;

namespace RoboCleaner.Models;

/// <summary>
/// Defines a 2D vector containing both position (X,Y) and direction.
/// </summary>
/// <param name="X">X coordinate</param>
/// <param name="Y">Y coordinate</param>
/// <param name="Direction">Direction. <see cref="Enums.Direction"/></param>
public record PositionVector2D(int X, int Y, Direction Direction);
