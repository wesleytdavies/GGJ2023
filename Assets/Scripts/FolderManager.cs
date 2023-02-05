using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WesleyDavies;

public class FolderManager : MonoBehaviour
{
    /// <summary>
    /// Should start empty.
    /// </summary>
    public List<Folder> VirusFolders { get; private set; }
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
    [SerializeField] private Color _cureColor;

    private void Awake()
    {
        Services.FolderManager = this;
        VirusFolders = new();
        FirewallFolders = new();
        //CureFolders = new();
    }

    private void Start()
    {
        ValidFirewallFolders = new(Services.Map.Folders);
        ValidCureFolders = new(Services.Map.Folders);
        UnpickedFolders = new(Services.Map.Folders);
    }

    public void AssignCureFolder(Firewall firewall)
    {
        Folder cureFolder = UnpickedFolders.PickRandom();
        UnpickedFolders.Remove(cureFolder);
        cureFolder.isCureFolder = true;
        cureFolder.cureFirewall = firewall;
        cureFolder.SpriteRenderer.color = _cureColor;
        firewall.cureFolder = cureFolder;
    }

    public void RefillFirewallFolders()
    {
        UnpickedFolders = new(Services.Map.Folders);
    }
    //public void RefillCureFolders()
    //{
    //    UnpickedFolders = new(ValidCureFolders);
    //}
}
