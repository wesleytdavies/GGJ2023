using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using WesleyDavies;

/// <summary>
/// A map is a collection of folders
/// </summary>
public class Map : MonoBehaviour
{
    [Tooltip("Cell of the Grid that the Map starts on.")]
    [SerializeField] private Vector2Int _startCellPosition;
    //[Tooltip("How many root folders should there be?")]
    //[SerializeField] private int _maxRootFolders;
    [Tooltip("How many non-root folders to put on the map.")]
    [SerializeField] private int _maxFolders;
    public int MaxChildCount
    {
        get => _maxChildCount;
    }
    [Tooltip("How many children each folder can have.")]
    [SerializeField] private int _maxChildCount;
    [Tooltip("How far away each child can be from its parent.")]
    [SerializeField] private Vector2Int _childDistanceRange;

    public List<Folder> Folders { get; private set; }
    public Grid Grid { get; private set; }
    [SerializeField] private ValuesSO _values;
    private int _rootIndex = 0;
    public GridPath[] Paths { get; private set; }
    public Cell HumanCell { get; private set; }

    private void Start()
    {
        Folders = new();
        Grid = new(_values, _values.GridOrigin, this);
        PopulateMap(_maxFolders);
        Paths = CreatePaths();
    }

    private void PopulateMap(int foldersToCreate)
    {
        //for (int i = 0; i < _rootFolderCount; i++)
        //{

        //}

        HumanCell = Grid.cells[_startCellPosition.x, _startCellPosition.y];

        Folder previousFolder = Instantiate(_values.humanFolderPrefab, transform);
        previousFolder.transform.position = transform.InverseTransformPoint(HumanCell.WorldPosition);
        previousFolder.cell = HumanCell;
        previousFolder.cell.occupant = previousFolder;
        previousFolder.map = this;
        Folders.Add(previousFolder);

        for (int i = 0; i < foldersToCreate; i++)
        {
            Vector2Int distanceRange = _childDistanceRange;
            if (previousFolder.cell == HumanCell)
            {
                distanceRange.x = distanceRange.x < _values.HumanCellRadius ? _values.HumanCellRadius : distanceRange.x;
                distanceRange.y = distanceRange.y < _values.HumanCellRadius ? _values.HumanCellRadius : distanceRange.y;
            }
            Cell newFolderCell = PickNewCell(previousFolder.cell, distanceRange);
            //Folder newFolder = new(newFolderCell);
            Folder newFolder = Instantiate(_values.folderPrefab, transform);
            newFolder.transform.position = transform.InverseTransformPoint(newFolderCell.WorldPosition);
            newFolder.cell = newFolderCell;
            newFolder.cell.occupant = newFolder;
            newFolder.map = this;
            previousFolder.Children.Add(newFolder);
            newFolder.parent = previousFolder;
            Folders.Add(newFolder);

            if (!previousFolder.CanHaveChild)
            {
                _rootIndex++;
                previousFolder = Folders[_rootIndex];
            }
            //previousCell = newFolderCell;
        }
    }

    //TODO: add special radius around human foolder

    private GridPath[] CreatePaths()
    {
        List<GridPath> paths = new();

        foreach(Folder folder in Folders)
        {
            if (folder.Children.Count <= 0)
            {
                continue;
            }
            foreach(Folder child in folder.Children)
            {
                GridPath newGridPath;
                if(folder.cell.Position.x == child.cell.Position.x || folder.cell.Position.y == child.cell.Position.y)
                {
                    newGridPath = new(folder.cell, child.cell);
                    paths.Add(newGridPath);
                    child.pathToParent = newGridPath;
                    continue;
                }
                int coinFlip = Random.Range(0, 2);
                Cell turningCell;
                switch (coinFlip)
                {
                    case 0:
                        turningCell = Grid.cells[folder.cell.Position.x, child.cell.Position.y];
                        break;
                    case 1:
                        turningCell = Grid.cells[child.cell.Position.x, folder.cell.Position.y];
                        break;
                    default:
                        turningCell = null;
                        break;
                }
                newGridPath = new(folder.cell, child.cell, turningCell);
                child.pathToParent = newGridPath;
                paths.Add(newGridPath);
            }
        }

        //foreach(GridPath path in paths)
        //{
        //    if (path.Turns.Length <= 0)
        //    {
        //        Debug.Log("Start: " + path.ends[0].Position + ", End: " + path.ends[1].Position);
        //    }
        //    else
        //    {
        //        Debug.Log("Start: " + path.ends[0].Position + ", End: " + path.ends[1].Position + ", Turn: " + path.Turns[0].Position);
        //    }
        //}
        return paths.ToArray();
    }

    private Cell PickNewCell(Cell previousCell, Vector2Int distanceRange)
    {
        return GetRandomCell(GetPossibleCellsFromDistanceRange(Grid, previousCell, distanceRange));
    }

    private Cell[] GetPossibleCellsFromDistanceRange(Grid grid, Cell startCell, Vector2Int distanceRange)
    {
        List<Cell> validPossibleCells = new();

        Vector2Int xBounds = new()
        {
            x = startCell.Position.x - distanceRange.y > 0 ? startCell.Position.x - distanceRange.y : 0,
            y = startCell.Position.x + distanceRange.y < _values.CellCount.x - 1 ? startCell.Position.x + distanceRange.y : _values.CellCount.x - 1
        };
        Vector2Int yBounds = new()
        {
            x = startCell.Position.y - distanceRange.y > 0 ? startCell.Position.y - distanceRange.y : 0,
            y = startCell.Position.y + distanceRange.y < _values.CellCount.y - 1 ? startCell.Position.y + distanceRange.y : _values.CellCount.y - 1
        };

        for (int i = xBounds.x; i <= xBounds.y; i++)
        {
            for (int j = yBounds.x; j <= yBounds.y; j++)
            {
                if (Mathf.Abs(i - startCell.Position.x) + Mathf.Abs(j - startCell.Position.y) < distanceRange.x)
                {
                    continue;
                }
                if (Mathf.Abs(i - startCell.Position.x) + Mathf.Abs(j - startCell.Position.y) > distanceRange.y)
                {
                    continue;
                }
                if (grid.cells[i, j].IsOccupied)
                {
                    continue;
                }
                if (!grid.cells[i, j].CanBeOccupied)
                {
                    continue;
                }

                //TODO: check if adjacent cells are occupied as well?
                //3 empty cells around each folder (in any direction: folder 1 2 3)
                validPossibleCells.Add(grid.cells[i, j]);
            }
        }
        return validPossibleCells.ToArray();
    }

    private Cell GetRandomCell(Cell[] cells)
    {   
        return cells.PickRandom();
    }
}
