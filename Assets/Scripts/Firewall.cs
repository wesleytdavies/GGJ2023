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

    public Folder cureFolder;

    public float MaxRadius
    {
        get => _maxRadius;
    }
    [SerializeField] private float _maxRadius;

    public float ExpandSpeed
    {
        get => _expandSpeed;
    }
    [Tooltip("How quickly the radius expands in Unity units per second.")]
    [SerializeField] private float _expandSpeed;

    //TODO: Firewall expansion
    //firewall starts at random folder
    //cure folder
    //infect effect (filling up folder)

    private void Start()
    {
        StartCoroutine(Expand());
    }

    private IEnumerator Expand()
    {
        Vector2 radius = new Vector2(transform.localScale.x, transform.localScale.z);
        while (radius.x < MaxRadius)
        {
            radius.x += _expandSpeed * Time.fixedDeltaTime;
            radius.y += _expandSpeed * Time.fixedDeltaTime;
            transform.localScale = new Vector3(radius.x, transform.localScale.y, radius.y);
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
}
