using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanScreen : MonoBehaviour
{
    public GameObject humanCursor;
    public Map map;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SpawnHumanCursor();
        }
    }

    public void SpawnHumanCursor()
    {
        //Get a random set of path from the GridPath on Map
        int randomNumber = Random.Range(4, 9);
        GridPath randomPath = map.Paths[randomNumber];
        Vector2 startPoint = new Vector2(randomPath.ends[0].WorldPosition.x, randomPath.ends[0].WorldPosition.z);
        Vector2 turnPoint = new Vector2(randomPath.Turns[0].WorldPosition.x, randomPath.Turns[0].WorldPosition.z);
        Vector2 endPoint = new Vector2(randomPath.ends[1].WorldPosition.x, randomPath.ends[1].WorldPosition.z);

        //Spawn a Cursor and set its waypoints
        GameObject cursor = Instantiate(humanCursor);
        List<Vector2> cursorPath = cursor.GetComponent<HumanMovement>().waypoints;
        cursorPath[0] = new Vector2(0, 0);
        //cursorPath[1] = startPoint;
        cursorPath[1] = turnPoint;
        cursorPath[2] = endPoint;
    }
}
