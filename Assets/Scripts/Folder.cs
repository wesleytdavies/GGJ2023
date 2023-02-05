using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Base class for folders
/// </summary>
public class Folder : CellOccupant
{
    public SpriteRenderer SpriteRenderer
    {
        get => _spriteRenderer;
    }
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public Color OriginalColor { get; private set; }

    [NonSerialized] public Folder parent;
    public List<Folder> Children { get; private set; }
    [NonSerialized] public Cell cell;

    [NonSerialized] public GridPath pathToParent;

    [NonSerialized] public bool isCureFolder = false;
    [NonSerialized] public Firewall cureFirewall;

    [SerializeField] private GameObject _folderFill;
    public SpriteRenderer CureIcon
    {
        get => _cureIcon;
    }
    [SerializeField] private SpriteRenderer _cureIcon;
    public SpriteRenderer FireIcon
    {
        get => _fireIcon;
    }
    [SerializeField] private SpriteRenderer _fireIcon;

    public bool CanHaveChild
    {
        get => Children.Count < map.MaxChildCount;
    }

    public bool IsFilling { get; private set; }
    public bool IsInfected
    {
        get => Services.FolderManager.InfectedFolders.Contains(this);
    }
    public IEnumerator FillCoroutine { get; private set; }
    public IEnumerator UnfillCoroutine { get; private set; }

    private void Awake()
    {
        Children = new();
        OriginalColor = SpriteRenderer.color;
        FillCoroutine = Fill();
        UnfillCoroutine = Unfill();
        IsFilling = false;
    }

    private void Start()
    {
        CureIcon.enabled = false;
        FireIcon.enabled = false;
        float startPositionY = Services.FolderManager.StartFolderFillY;
        Vector3 fillWorldPosition = _folderFill.transform.parent.TransformPoint(new Vector3(0f, startPositionY, 0f));
        _folderFill.transform.position = new Vector3(_folderFill.transform.position.x, fillWorldPosition.y, _folderFill.transform.position.z);
        //StartCoroutine(FillCoroutine);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void Uncure()
    {
        CureIcon.enabled = false;
        SpriteRenderer.color = OriginalColor;
        isCureFolder = false;
        if (!Services.FolderManager.UnpickedFolders.Contains(this))
        {
            Services.FolderManager.UnpickedFolders.Add(this);
        }
        cureFirewall = null;
    }

    private IEnumerator Fill()
    {
        IsFilling = true;
        if (Services.FolderManager.UnpickedFolders.Contains(this))
        {
            Services.FolderManager.UnpickedFolders.Remove(this);
        }
        while (_folderFill.transform.localPosition.y < Services.FolderManager.EndFolderFillY)
        {
            float newLocalY = _folderFill.transform.localPosition.y + Services.FolderManager.FolderFillSpeed * Time.fixedDeltaTime;
            _folderFill.transform.position = _folderFill.transform.parent.TransformPoint(new Vector3(0f, newLocalY, 0f));
            yield return new WaitForFixedUpdate();
        }
        _folderFill.transform.position = _folderFill.transform.parent.TransformPoint(new Vector3(0f, Services.FolderManager.EndFolderFillY, 0f));
        IsFilling = false;
        Services.FolderManager.onFolderFill?.Invoke(this);
        yield break;
    }

    private IEnumerator Unfill()
    {
        StopCoroutine(FillCoroutine);
        IsFilling = false;
        while (_folderFill.transform.localPosition.y > Services.FolderManager.StartFolderFillY)
        {
            float newLocalY = _folderFill.transform.localPosition.y - Services.FolderManager.FolderUnfillSpeed * Time.fixedDeltaTime;
            _folderFill.transform.position = _folderFill.transform.parent.TransformPoint(new Vector3(0f, newLocalY, 0f));
            yield return new WaitForFixedUpdate();
        }
        _folderFill.transform.position = _folderFill.transform.parent.TransformPoint(new Vector3(_folderFill.transform.position.x, Services.FolderManager.StartFolderFillY, _folderFill.transform.position.z));
        if (!Services.FolderManager.UnpickedFolders.Contains(this))
        {
            Services.FolderManager.UnpickedFolders.Add(this);
        }
        yield break;
    }

    //when the player collides with a folder
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (isCureFolder)
            {
                col.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                foreach (GameObject patrol in GameObject.FindGameObjectsWithTag("Patroler"))
                {
                    if (patrol.name == "PatrolDefender")
                    {
                        patrol.gameObject.GetComponent<PatrolMovement>().isAlarmed = false;
                    }
                }
            }

            //Debug.Log("Start Fill");
            if (!IsInfected)
            {
                StartCoroutine(FillCoroutine);
            }
        }
    }

    //when the player exits collision with a folder
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            col.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
            StartCoroutine(UnfillCoroutine);
            //Debug.Log("Leave Fill");
            if (!IsInfected && IsFilling)
            {

            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            foreach (GameObject patrol in GameObject.FindGameObjectsWithTag("Patroler"))
            {
                if (patrol.name == "PatrolDefender")
                {
                    patrol.gameObject.GetComponent<PatrolMovement>().isAlarmed = false;
                }
            }
        }
    }
}
