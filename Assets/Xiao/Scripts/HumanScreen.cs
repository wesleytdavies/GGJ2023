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
        Debug.Log("SpawnCursor");
        //Get a random set of path from the GridPath on Map
        int randomNumber = Random.Range(4, map.Folders.Count-1);
        Folder randomFolder = map.Folders[randomNumber];
        //Debug.Log(randomNumber);

        //Spawn a Cursor and set its waypoints
        GameObject cursor = Instantiate(humanCursor);
        CreatePathForCursor(cursor, randomFolder, randomNumber);
    }

    public void CreatePathForCursor(GameObject cursor, Folder randomFolder, int folderCount)
    {
        List<Folder> allPathFolders = new List<Folder>();
        //check how many parents folders does the randomFolder have
        int parentsToCheck = 0;
        int i = 0;
        while (i < folderCount)
        {
            i += (int)Mathf.Pow(map.MaxChildCount, parentsToCheck);
            parentsToCheck++;
        }

        Folder childFolder = randomFolder;
        for (int j = parentsToCheck; j > 0; j--)
        {
            allPathFolders.Add(childFolder.parent);
            childFolder = childFolder.parent;
        }

        //List<Vector2> cursorPath = cursor.GetComponent<HumanMovement>().waypoints;
        Debug.Log(allPathFolders.Count);
        foreach (Folder folder in allPathFolders)
        {

            /*            cursorPath.Add(new Vector2(folder.pathToParent.ends[0].WorldPosition.x, folder.pathToParent.ends[0].WorldPosition.z));
                        cursorPath.Add(new Vector2(folder.pathToParent.Turns[0].WorldPosition.x, folder.pathToParent.Turns[0].WorldPosition.z));
                        cursorPath.Add(new Vector2(folder.pathToParent.ends[1].WorldPosition.x, folder.pathToParent.ends[1].WorldPosition.z));*/
            cursor.GetComponent<HumanMovement>().waypoints.Add(new Vector2(folder.pathToParent.ends[0].WorldPosition.x, folder.pathToParent.ends[0].WorldPosition.z));
            cursor.GetComponent<HumanMovement>().waypoints.Add(new Vector2(folder.pathToParent.Turns[0].WorldPosition.x, folder.pathToParent.Turns[0].WorldPosition.z));
            cursor.GetComponent<HumanMovement>().waypoints.Add(new Vector2(folder.pathToParent.ends[1].WorldPosition.x, folder.pathToParent.ends[1].WorldPosition.z));
        }

    }

}
