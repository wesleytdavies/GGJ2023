using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WesleyDavies;

public class FirewallManager : MonoBehaviour
{
    [Tooltip("How long after the player moves until the first firewall is spawned.")]
    [SerializeField] private float _firstFirewallSpawnTime;
    [Tooltip("How long after the last firewall spawned that the next one should.")]
    [SerializeField] private float _firewallSpawnTime;
    [SerializeField] private GameObject _firewallPrefab;
    //[Tooltip("How far away from the player's start position the first firewall should go.")]
    //[SerializeField] private int _minimumFirstFirewallRadius;
    public IEnumerator StartFirewallCoroutine { get; private set; }
    public float ExpandSpeed
    {
        get => _expandSpeed;
    }
    [Tooltip("How quickly the radius expands in Unity units per second.")]
    [SerializeField] private float _expandSpeed;
    public float RetractSpeed
    {
        get => _retractSpeed;
    }
    [Tooltip("How quickly the radius expands in Unity units per second.")]
    [SerializeField] private float _retractSpeed;

    private void Awake()
    {
        Services.FirewallManager = this;
        StartFirewallCoroutine = StartFirewall();
    }

    private void Start()
    {
        StartCoroutine(StartFirewallCoroutine);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// Should be called when the player first moves
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartFirewall()
    {
        yield return new WaitForSeconds(_firstFirewallSpawnTime);
        //SpawnFirstFirewall();
        SpawnFirewall();
        while (true)
        {
            yield return new WaitForSeconds(_firewallSpawnTime);
            SpawnFirewall();
        }
    }

    //private void SpawnFirstFirewall()
    //{
    //    if (Services.FolderManager.UnpickedFirewallFolders.Count <= 0)
    //    {
    //        Services.FolderManager.RefillFirewallFolders();
    //    }
    //    Folder spawnFolder = Services.FolderManager.UnpickedFirewallFolders.PickRandom();
    //    Instantiate(_firewallPrefab, spawnFolder.transform.position, Quaternion.identity);
    //    Services.FolderManager.UnpickedFirewallFolders.Remove(spawnFolder);
    //    Services.FolderManager.FirewallFolders.Add(spawnFolder);
    //}

    private void SpawnFirewall()
    {
        if (Services.FolderManager.UnpickedFolders.Count <= 0)
        {
            Services.FolderManager.RefillFirewallFolders();
        }
        Folder spawnFolder = Services.FolderManager.UnpickedFolders.PickRandom();
        Services.FolderManager.ValidCureFolders.Remove(spawnFolder);
        GameObject newFirewallObject = Instantiate(_firewallPrefab, spawnFolder.transform.position, Quaternion.identity);
        Firewall newFirewall = newFirewallObject.GetComponentInChildren<Firewall>();
        newFirewall.spawnFolder = spawnFolder;
        Services.FolderManager.UnpickedFolders.Remove(spawnFolder);
        Services.FolderManager.FirewallFolders.Add(spawnFolder);
        Services.FolderManager.AssignCureFolder(newFirewall);
    }
}
