using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Values Scriptable Object", menuName = "ScriptableObjects/Values", order = 0)]
public class ValuesSO : ScriptableObject
{
    //[Header("Cell")]
    public int CellSize
    {
        get => _cellSize;
        private set => _cellSize = value;
    }
    [Tooltip("Size in Unity units.")]
    [SerializeField] private int _cellSize;

    public Vector2Int CellCount
    {
        get => _cellCount;
        private set => _cellCount = value;
    }
    [Tooltip("Number of cells in the X and Y direction.")]
    [SerializeField] private Vector2Int _cellCount;

    public Vector3 GridOrigin
    {
        get => _gridOrigin;
        private set => _gridOrigin = value;
    }
    [Tooltip("Bottom left of the grid in world space.")]
    [SerializeField] private Vector3 _gridOrigin;

    public Folder humanFolderPrefab;
    public Folder folderPrefab;
    public CellOccupant turnPrefab;

    public int HumanCellRadius
    {
        get => _humanCellRadius;
    }
    [Tooltip("How many cells around the human cell cannot be occupied.")]
    [SerializeField] private int _humanCellRadius;

    public int OccupiedCellRadius
    {
        get => _occupiedCellRadius;
    }
    [Tooltip("How many cells around an occupied cell cannot be occupied.")]
    [SerializeField] private int _occupiedCellRadius;
}
