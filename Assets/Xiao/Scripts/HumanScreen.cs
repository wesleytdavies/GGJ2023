using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

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
        int parentsToCheck = -1;
        int i = 0;
        while (i < folderCount)
        {
            parentsToCheck++;
            i += (int)Mathf.Pow(map.MaxChildCount, parentsToCheck);
        }
        Folder childFolder = randomFolder;
        for (int j = parentsToCheck; j > 0; j--)
        {
            allPathFolders.Add(childFolder);
            childFolder = childFolder.parent;
        }

        //List<Vector2> cursorPath = cursor.GetComponent<HumanMovement>().waypoints;
        foreach (Folder folder in allPathFolders)
        {

            /*            cursorPath.Add(new Vector2(folder.pathToParent.ends[0].WorldPosition.x, folder.pathToParent.ends[0].WorldPosition.z));
                        cursorPath.Add(new Vector2(folder.pathToParent.Turns[0].WorldPosition.x, folder.pathToParent.Turns[0].WorldPosition.z));
                        cursorPath.Add(new Vector2(folder.pathToParent.ends[1].WorldPosition.x, folder.pathToParent.ends[1].WorldPosition.z));*/
            //if (cursor == null)
            //{
            //    Debug.Log("cursor null");
            //}
            //if (cursor.GetComponent<HumanMovement>().waypoints == null)
            //{
            //    Debug.Log("waypoints null");
            //}
            //if (folder.pathToParent == null)
            //{
            //    Debug.Log("path null");
            //}
            //cursor.GetComponent<HumanMovement>().waypoints.Add(new Vector2(folder.pathToParent.ends[0].WorldPosition.x, folder.pathToParent.ends[0].WorldPosition.z));
            //if (folder.pathToParent.Turns.Length > 0)
            //{
            //    cursor.GetComponent<HumanMovement>().waypoints.Add(new Vector2(folder.pathToParent.Turns[0].WorldPosition.x, folder.pathToParent.Turns[0].WorldPosition.z));
            //}
            //cursor.GetComponent<HumanMovement>().waypoints.Add(new Vector2(folder.pathToParent.ends[1].WorldPosition.x, folder.pathToParent.ends[1].WorldPosition.z));
        }
        for (int j = 0; j < allPathFolders.Count; j++)
        {
            cursor.GetComponent<HumanMovement>().waypoints.Insert(0, new Vector2(allPathFolders[j].pathToParent.ends[1].transform.position.x, allPathFolders[j].pathToParent.ends[1].transform.position.y));
            if (allPathFolders[j].pathToParent.Turns.Length > 0)
            {
                cursor.GetComponent<HumanMovement>().waypoints.Insert(0, new Vector2(allPathFolders[j].pathToParent.Turns[0].transform.position.x, allPathFolders[j].pathToParent.Turns[0].transform.position.y));
            }
            cursor.GetComponent<HumanMovement>().waypoints.Insert(0, new Vector2(allPathFolders[j].pathToParent.ends[0].transform.position.x, allPathFolders[j].pathToParent.ends[0].transform.position.y));
        }
        cursor.GetComponent<HumanMovement>().waypoints.Insert(0, new Vector2(map.Folders[0].transform.position.x, map.Folders[0].transform.position.y));
    }

}
