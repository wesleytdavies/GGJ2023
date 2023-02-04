using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    public CapsuleCollider CapsuleCollider
    {
        get => _capsuleCollider;
    }
    [SerializeField] private CapsuleCollider _capsuleCollider;

    //TODO: Firewall expansion
    //firewall starts at random folder
    //cure folder
    //infect effect (filling up folder)

    private IEnumerator Expand()
    {
        yield break;
    }
}
