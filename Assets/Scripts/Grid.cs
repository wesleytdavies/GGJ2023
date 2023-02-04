using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public ValuesSO Values { get; private set; }

    /// <summary>
    /// Grid origin is bottom left.
    /// </summary>
    public Vector3 Origin { get; private set; }
    public Cell[,] cells;

    public Grid(ValuesSO values, Vector3 origin)
    {
        Values = values;
        Origin = origin;

        cells = new Cell[Values.CellCount.x, Values.CellCount.y];
        for (int i = 0; i < Values.CellCount.x; i++)
        {
            for (int j = 0; j < Values.CellCount.y; j++)
            {
                cells[i, j] = new(this, new Vector2Int(i, j));
            }
        }
    }
}
