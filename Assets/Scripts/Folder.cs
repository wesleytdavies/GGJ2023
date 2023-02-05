using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Base class for folders
/// </summary>
public class Folder : CellOccupant
{
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Color OriginalColor { get; private set; }

    public Folder parent;
    public List<Folder> Children { get; private set; }
    public Cell cell;

    public GridPath pathToParent;

    public bool isCureFolder = false;
    public Firewall cureFirewall;

    [SerializeField] private GameObject _folderFill;

    //[Tooltip("Rank 0 is the main folder.")]
    //public int rank;

    //public struct CureFolder
    //{
    //    public readonly Folder Folder;
    //    public readonly Firewall Firewall;

    //    public CureFolder(Folder folder, Firewall firewall)
    //    {
    //        Folder = folder;
    //        Firewall = firewall;
    //    }
    //}

    public bool CanHaveChild
    {
        get => Children.Count < map.MaxChildCount;
    }

    private void Awake()
    {
        Children = new();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        OriginalColor = SpriteRenderer.color;
    }

    private void Update()
    {
        if (isCureFolder)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Cure();
            }
        }
    }

    private IEnumerator Fill()
    {
        yield break;
    }

    public void Cure()
    {
        SpriteRenderer.color = OriginalColor;
        isCureFolder = false;
        Services.FolderManager.UnpickedFolders.Add(this);
        Services.FolderManager.UnpickedFolders.Add(cureFirewall.spawnFolder);
        StopCoroutine(cureFirewall.ExpandCoroutine);
        StartCoroutine(cureFirewall.RetractCoroutine);
        cureFirewall = null;
    }

    public void Uncure()
    {
        SpriteRenderer.color = OriginalColor;
        isCureFolder = false;
        Services.FolderManager.UnpickedFolders.Add(this);
        Services.FolderManager.UnpickedFolders.Add(cureFirewall.spawnFolder);
        StopCoroutine(cureFirewall.ExpandCoroutine);
        StartCoroutine(cureFirewall.RetractCoroutine);
        cureFirewall = null;
    }
}
