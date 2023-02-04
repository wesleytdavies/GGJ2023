using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovement : MonoBehaviour
{
    public List<Vector2> waypoints;
    public float Speed = 2;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[0];
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 destination = waypoints[index];
        Vector3 newpos = Vector3.MoveTowards(transform.position, destination, Speed * Time.deltaTime);
        transform.position = newpos;

        float distance = Vector3.Distance(transform.position, destination);
        if(distance <= 0.05)
        {
            if(index < waypoints.Count-1)
            index++;
            else
            {
                transform.position = waypoints[0];
                index = 0;
            }
        }
    }
}
