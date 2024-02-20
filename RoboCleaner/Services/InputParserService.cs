using RoboCleaner.Enums;
using RoboCleaner.Models;

namespace RoboCleaner.Services;

public class InputParserService
{
    private const Direction DefaultStartingDirection = Direction.Right;

    public Grid2D Parse(string[] input)
    {
        int startX = 0;
        int startY = 0;
        var cells = new Cell[input.Length][];

        for (int row = 0; row < input.Length; row++)
        {
            cells[row] = new Cell[input[row].Length];
            for (int column = 0; column < cells[row].Length; column++)
            {
                cells[row][column] = new Cell { IsObstacle = (CellType)input[row][column] == CellType.Obstacle };
                if ((CellType)input[row][column] == CellType.StartingPoint)
                {
                    startX = column;
                    startY = row;
                }
            }
        }
        return new Grid2D(cells, new PositionVector2D(startX, startY, DefaultStartingDirection));
    }
}
