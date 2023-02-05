using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailDetect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
/*        if (col.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }*/
        if (col.gameObject.CompareTag("Patroler"))
        {
            Debug.Log("Hit Patroler!");
            col.gameObject.GetComponent<PatrolMovement>().isAlarmed = true;
        }
    }
}
