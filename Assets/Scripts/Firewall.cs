using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    //public CapsuleCollider CapsuleCollider
    //{
    //    get => _capsuleCollider;
    //}
    //private CapsuleCollider _capsuleCollider;

    private SpriteMask _mask;

    public Folder spawnFolder;
    public Folder cureFolder;

    public float MaxRadius
    {
        get => _maxRadius;
    }
    [SerializeField] private float _maxRadius;

    public IEnumerator ExpandCoroutine { get; private set; }
    public IEnumerator RetractCoroutine { get; private set; }
    public IEnumerator CureCoroutine;

    //TODO: Firewall expansion
    //firewall starts at random folder
    //cure folder
    //infect effect (filling up folder)

    private void Start()
    {
        _mask = transform.parent.GetComponentInChildren<SpriteMask>();
        _mask.transform.localScale = transform.localScale;
        ExpandCoroutine = Expand();
        RetractCoroutine = Retract();
        StartCoroutine(ExpandCoroutine);
    }

    /// <summary>
    /// Should be called when Firewall touches player.
    /// </summary>
    public void FoundPlayer()
    {
        cureFolder.Uncure();
    }

    private IEnumerator Expand()
    {
        Vector2 radius = new Vector2(transform.localScale.x, transform.localScale.y);
        while (radius.x < MaxRadius)
        {
            radius.x += Services.FirewallManager.ExpandSpeed * Time.fixedDeltaTime;
            radius.y += Services.FirewallManager.ExpandSpeed * Time.fixedDeltaTime;
            Vector3 newScale = new Vector3(radius.x, radius.y, transform.localScale.z);
            transform.localScale = newScale;
            _mask.transform.localScale = newScale;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Retract()
    {
        Vector2 radius = new Vector2(transform.localScale.x, transform.localScale.y);
        while (radius.x > 0f)
        {
            radius.x -= Services.FirewallManager.RetractSpeed * Time.fixedDeltaTime;
            radius.y -= Services.FirewallManager.RetractSpeed * Time.fixedDeltaTime;
            Vector3 newScale = new Vector3(radius.x, radius.y, transform.localScale.z);
            transform.localScale = newScale;
            _mask.transform.localScale = newScale;
            yield return new WaitForFixedUpdate();
        }
        Destroy(transform.parent.gameObject);
        yield break;
    }
}
