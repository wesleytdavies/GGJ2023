using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private Grid _grid;

    public Vector2Int Position { get; private set; }
    public Vector3 WorldPosition
    {
        get => new(Position.x * _grid.Values.CellSize + _grid.Origin.x, 0f, Position.y * _grid.Values.CellSize + _grid.Origin.y);
    }
    //public Vector3 WorldPosition
    //{
    //    get => _grid.Map.transform.TransformPoint(LocalPosition);
    //}

    //public Vector3 LocalPosition
    //{
    //    get => new(Position.x * _grid.Values.CellSize + _grid.Origin.x, 0f, Position.y * _grid.Values.CellSize + _grid.Origin.y);
    //}
    //public Vector3 WorldPosition
    //{
    //    get => _grid.Map.transform.TransformPoint(LocalPosition);
    //}

    public CellOccupant occupant;

    public bool IsOccupied
    {
        get => occupant != null;
    }

    public bool CanBeOccupied
    {
        get => !Array.Exists(_grid.GetCellsInRadius(this, _grid.Values.OccupiedCellRadius), x => x.IsOccupied) && !Array.Exists(_grid.GetCellsInRadius(_grid.Map.HumanCell, _grid.Values.HumanCellRadius - 1), x => x == this);
    }

    public Cell(Grid grid, Vector2Int position)
    {
        _grid = grid;
        Position = position;
        Paths = new();
    }

    public List<GridPath> Paths { get; private set; }
}
