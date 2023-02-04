using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Base class for folders
/// </summary>
public class Folder : CellOccupant
{
    public Folder parent;
    public List<Folder> Children { get; private set; }
    public Cell cell;

    public GridPath pathToParent;

    [SerializeField] private GameObject _folderFill;

    //[Tooltip("Rank 0 is the main folder.")]
    //public int rank;

    public bool CanHaveChild
    {
        get => Children.Count < map.MaxChildCount;
    }

    private void Awake()
    {
        Children = new();
    }

    private IEnumerator Fill()
    {
        yield break;
    }
}
