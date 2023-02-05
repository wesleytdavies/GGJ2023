using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WesleyDavies;

public class FolderManager : MonoBehaviour
{
    /// <summary>
    /// Should start empty.
    /// </summary>
    public List<Folder> InfectedFolders { get; private set; }
    /// <summary>
    /// Should start empty.
    /// </summary>
    public List<Folder> FirewallFolders { get; private set; }
    /// <summary>
    /// Should start empty.
    /// </summary>
    //public List<Folder.CureFolder> CureFolders { get; private set; }
    /// <summary>
    /// Should start filled.
    /// </summary>
    public List<Folder> ValidFirewallFolders { get; private set; }
    //public List<Folder> UnpickedFirewallFolders { get; private set; }
    ///// <summary>
    ///// Should start filled.
    ///// </summary>
    //public List<Folder> UnpickedCureFolders { get; private set; }
    public List<Folder> UnpickedFolders { get; private set; }
    public List<Folder> ValidCureFolders { get; private set; }
    [Tooltip("Color of cure folders.")]
    [SerializeField] private Color _cureColor;
    public Color FireColor
    {
        get => _fireColor;
    }
    [Tooltip("Color of fire folders.")]
    [SerializeField] private Color _fireColor;
    public Color InfectedColor
    {
        get => _infectedColor;
    }
    [Tooltip("Color of the folder when virus has infected it.")]
    [SerializeField] private Color _infectedColor;

    public delegate void FolderFill(Folder folder);
    public FolderFill onFolderFill;

    public float FolderFillSpeed
    {
        get => _folderFillSpeed;
    }
    [Tooltip("How quickly the folder should fill up in units per second.")]
    [SerializeField] private float _folderFillSpeed;

    public float FolderUnfillSpeed
    {
        get => _folderUnfillSpeed;
    }
    [Tooltip("How quickly the folder should fill up in units per second.")]
    [SerializeField] private float _folderUnfillSpeed;

    public float StartFolderFillY
    {
        get => _startFolderFillY;
    }
    [SerializeField] private float _startFolderFillY;

    public float EndFolderFillY
    {
        get => _endFolderFillY;
    }
    [SerializeField] private float _endFolderFillY;

    private void Awake()
    {
        Services.FolderManager = this;
        InfectedFolders = new();
        FirewallFolders = new();
        //CureFolders = new();
    }

    private void Start()
    {
        ValidFirewallFolders = new(Services.Map.Folders);
        ValidCureFolders = new(Services.Map.Folders);
        UnpickedFolders = new(Services.Map.Folders);
        UnpickedFolders.RemoveAt(0); //human folder should not be valid

        onFolderFill += Fill;
        //onFolderFill += Cure;
    }

    private void OnDestroy()
    {
        onFolderFill -= Fill;
        //onFolderFill -= Cure;
    }

    public void AssignCureFolder(Firewall firewall)
    {
        Folder cureFolder = UnpickedFolders.PickRandom();
        UnpickedFolders.Remove(cureFolder);
        cureFolder.isCureFolder = true;
        cureFolder.CureIcon.enabled = true;
        cureFolder.cureFirewall = firewall;
        cureFolder.SpriteRenderer.color = _cureColor;
        firewall.cureFolder = cureFolder;
    }

    //public void RefillFirewallFolders()
    //{
    //    UnpickedFolders = new(Services.Map.Folders);
    //}
    //public void RefillCureFolders()
    //{
    //    UnpickedFolders = new(ValidCureFolders);
    //}

    private void Cure(Folder folder)
    {
        folder.isCureFolder = false;
        if (!UnpickedFolders.Contains(folder.cureFirewall.spawnFolder))
        {
            UnpickedFolders.Add(folder.cureFirewall.spawnFolder);
        }
        StartCoroutine(folder.cureFirewall.RetractCoroutine);
        folder.cureFirewall = null;
    }

    private void Fill(Folder folder)
    {
        folder.SpriteRenderer.color = InfectedColor;
        if (!InfectedFolders.Contains(folder))
        {
            InfectedFolders.Add(folder);
        }
        if (folder.isCureFolder)
        {
            Cure(folder);
        }
    }
}
