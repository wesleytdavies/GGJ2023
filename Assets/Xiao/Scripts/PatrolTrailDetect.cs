using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTrailDetect : MonoBehaviour
{
    public PatrolMovement _patroler;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player!");
            _patroler.isAlarmed = true;
        }
    }
}
