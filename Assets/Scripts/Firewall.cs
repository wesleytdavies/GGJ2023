using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    //public CapsuleCollider CapsuleCollider { get; private set; }
    //public Rigidbody2D Rb { get; private set; }
    public AudioSource AudioSource { get; private set; }

    private SpriteMask _mask;
    [SerializeField] private SpriteRenderer _texture;

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

    private void Awake()
    {
        //CapsuleCollider = GetComponent<CapsuleCollider>();
        //Rb = GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        spawnFolder.isRedFolder = true;
        _mask = transform.parent.GetComponentInChildren<SpriteMask>();
        _mask.transform.localScale = transform.localScale;
        ExpandCoroutine = Expand();
        RetractCoroutine = Retract();
        StartCoroutine(ExpandCoroutine);
/*        if (patrols == null)
        {
            patrols = GameObject.FindGameObjectsWithTag("Patroler");
        }*/
    }

        private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RetractCoroutine);
        }
    }

    private IEnumerator Expand()
    {
        Services.AudioManager.PlayFirewallExpand(this);
        Vector2 previousRadius = new Vector2(transform.localScale.x, transform.localScale.y);
        Vector2 radius = new Vector2(transform.localScale.x, transform.localScale.y);
        Vector2 textureRadius = new Vector2(_texture.transform.localScale.x, _texture.transform.localScale.y);
        while (radius.x < MaxRadius)
        {
            radius.x += Services.FirewallManager.ExpandSpeed * Time.fixedDeltaTime;
            radius.y += Services.FirewallManager.ExpandSpeed * Time.fixedDeltaTime;
            Vector3 newScale = new Vector3(radius.x, radius.y, transform.localScale.z);
            transform.localScale = newScale;
            _mask.transform.localScale = newScale;

            float deltaScale = radius.x / previousRadius.x;
            previousRadius = radius;
            textureRadius.x /= deltaScale;
            textureRadius.y /= deltaScale;
            _texture.transform.localScale = new Vector3(textureRadius.x, textureRadius.y, _texture.transform.localScale.z);
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
        StopCoroutine(ExpandCoroutine);
        Services.AudioManager.PlayFirewallRetract(this);
        Destroy(this.gameObject.GetComponent<CapsuleCollider2D>());
        //Rb. = false;
        Vector2 previousRadius = new Vector2(transform.localScale.x, transform.localScale.y);
        Vector2 radius = new Vector2(transform.localScale.x, transform.localScale.y);
        Vector2 textureRadius = new Vector2(_texture.transform.localScale.x, _texture.transform.localScale.y);
        while (radius.x > 0f)
        {
            radius.x -= Services.FirewallManager.RetractSpeed * Time.fixedDeltaTime;
            radius.y -= Services.FirewallManager.RetractSpeed * Time.fixedDeltaTime;
            Vector3 newScale = new Vector3(radius.x, radius.y, transform.localScale.z);
            transform.localScale = newScale;
            _mask.transform.localScale = newScale;

            float deltaScale = previousRadius.x / radius.x;
            previousRadius = radius;
            textureRadius.x *= deltaScale;
            textureRadius.y *= deltaScale;
            _texture.transform.localScale = new Vector3(textureRadius.x, textureRadius.y, _texture.transform.localScale.z);
            yield return new WaitForFixedUpdate();
        }
        if (!Services.FolderManager.UnpickedFolders.Contains(spawnFolder))
        {
            Services.FolderManager.UnpickedFolders.Add(spawnFolder);
        }
        spawnFolder.FireIcon.enabled = false;
        spawnFolder.SpriteRenderer.color = spawnFolder.IsInfected ? Services.FolderManager.InfectedColor : spawnFolder.OriginalColor;
        Destroy(transform.parent.gameObject);
        yield break;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger Firewall");
            foreach (GameObject patrol in GameObject.FindGameObjectsWithTag("Patroler"))
            {
                if (patrol.name == "PatrolDefender")
                {
                    patrol.gameObject.GetComponent<PatrolMovement>().isAlarmed = true;
                    Services.FirewallManager.onPlayerHit?.Invoke(this);
                }
            }
        }
    }

    //when the player exits collision with a folder
/*    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Leave Fire");

        }
    }*/
}
