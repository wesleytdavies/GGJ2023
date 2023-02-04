using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public ValuesSO Values { get; private set; }
    public Map Map { get; private set; }

    /// <summary>
    /// Grid origin is bottom left.
    /// </summary>
    public Vector3 Origin { get; private set; }
    public Cell[,] cells;

    public Grid(ValuesSO values, Vector3 origin, Map map)
    {
        Values = values;
        Origin = origin;
        Map = map;

        cells = new Cell[Values.CellCount.x, Values.CellCount.y];
        for (int i = 0; i < Values.CellCount.x; i++)
        {
            for (int j = 0; j < Values.CellCount.y; j++)
            {
                cells[i, j] = new(this, new Vector2Int(i, j));
            }
        }
    }

    public Cell[] GetCellsInRadius(Cell startCell, int radius)
    {
        List<Cell> cellsInRadius = new();

        Vector2Int xBounds = new()
        {
            x = startCell.Position.x - radius > 0 ? startCell.Position.x - radius : 0,
            y = startCell.Position.x + radius < Values.CellCount.x - 1 ? startCell.Position.x + radius : Values.CellCount.x - 1
        };
        Vector2Int yBounds = new()
        {
            x = startCell.Position.y - radius > 0 ? startCell.Position.y - radius : 0,
            y = startCell.Position.y + radius < Values.CellCount.y - 1 ? startCell.Position.y + radius : Values.CellCount.y - 1
        };

        for (int i = xBounds.x; i <= xBounds.y; i++)
        {
            for (int j = yBounds.x; j <= yBounds.y; j++)
            {
                if (Mathf.Abs(i - startCell.Position.x) + Mathf.Abs(j - startCell.Position.y) > radius)
                {
                    continue;
                }
                cellsInRadius.Add(cells[i, j]);
            }
        }
        return cellsInRadius.ToArray();
    }
}
