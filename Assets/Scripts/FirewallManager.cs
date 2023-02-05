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

    public delegate void PlayerHit(Firewall firewall);
    public PlayerHit onPlayerHit;

    private void Awake()
    {
        Services.FirewallManager = this;
        StartFirewallCoroutine = StartFirewall();
    }

    private void Start()
    {
        onPlayerHit += HitPlayer;
        StartCoroutine(StartFirewallCoroutine);
    }

    private void OnDestroy()
    {
        onPlayerHit -= HitPlayer;
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

    private void HitPlayer(Firewall firewall)
    {
        StartCoroutine(firewall.RetractCoroutine);
        firewall.cureFolder.Uncure();
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
            StopCoroutine(StartFirewallCoroutine);
            return;
            //Services.FolderManager.RefillFirewallFolders();
        }
        Folder spawnFolder = Services.FolderManager.UnpickedFolders.PickRandom();
        Services.FolderManager.ValidCureFolders.Remove(spawnFolder);
        GameObject newFirewallObject = Instantiate(_firewallPrefab, spawnFolder.transform.position, Quaternion.identity);
        Firewall newFirewall = newFirewallObject.GetComponentInChildren<Firewall>();
        newFirewall.spawnFolder = spawnFolder;
        spawnFolder.FireIcon.enabled = true;
        spawnFolder.SpriteRenderer.color = Services.FolderManager.FireColor;

        if (Services.FolderManager.UnpickedFolders.Contains(spawnFolder))
        {
            Services.FolderManager.UnpickedFolders.Remove(spawnFolder);
        }
        if (!Services.FolderManager.FirewallFolders.Contains(spawnFolder))
        {
            Services.FolderManager.FirewallFolders.Add(spawnFolder);
        }

        Services.FolderManager.AssignCureFolder(newFirewall);
    }
}
