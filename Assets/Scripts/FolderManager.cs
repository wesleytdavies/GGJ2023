using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderManager : MonoBehaviour
{
    public List<Folder> VirusFolders { get; private set; }
    public List<Folder> FirewallFolders { get; private set; }

    private void Start()
    {
        VirusFolders = new();
    }
}
