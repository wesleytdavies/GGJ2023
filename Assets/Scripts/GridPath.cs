using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPath
{
    //public Cell[] ends = new Cell[2];
    //public Cell[] Turns { get; private set; }

    //public GridPath(Cell end1, Cell end2, params Cell[] turns)
    //{
    //    ends[0] = end1;
    //    ends[1] = end2;
    //    Turns = turns;
    //}

    public CellOccupant[] ends = new CellOccupant[2];
    public CellOccupant[] Turns { get; private set; }

    public GridPath(CellOccupant end1, CellOccupant end2, params CellOccupant[] turns)
    {
        ends[0] = end1;
        ends[1] = end2;
        Turns = turns;
    }
}
