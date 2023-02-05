using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WiningCondition : MonoBehaviour
{
    Map map;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger Huaman");
            if (Services.FolderManager.InfectedFolders.Count >= map.Folders.Count)
            {
                SceneManager.LoadScene("Win Scene");
            }

        }
    }
}
