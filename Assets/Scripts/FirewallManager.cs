using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewallManager : MonoBehaviour
{
    [Tooltip("How long after the player moves until the first firewall is spawned.")]
    [SerializeField] private float _firstFirewallSpawnTime;
    [Tooltip("How long after the last firewall spawned that the next one should.")]
    [SerializeField] private float _firewallSpawnTime;
    [SerializeField] private Firewall _firewallPrefab;
    private List<Folder> _validFirewallFolders = new();

    private void Start()
    {

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
        //spawn firewall
        while (true)
        {
            yield return new WaitForSeconds(_firewallSpawnTime);
            //spawn firewall
        }
    }

    private void SpawnFirstFirewall()
    {

    }

    private void SpawnFirewall()
    {
        Instantiate(_firewallPrefab);
    }
}
