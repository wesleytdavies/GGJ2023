using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WiningCondition : MonoBehaviour
{
    Map map;
    public ParticleSystem particle;
    bool won = false;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        //PlayPartical();
    }

    // Update is called once per frame
    void Update()
    {
        if (Services.FolderManager.InfectedFolders.Count >= map.Folders.Count-1 && !won)
        {
            won = true;
            PlayPartical();
        }
    }
    void PlayPartical()
    {
        particle.Play();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger Huaman");
            if (Services.FolderManager.InfectedFolders.Count >= map.Folders.Count-1)
            {
                SceneManager.LoadScene("Win Scene");

            }

        }
    }
}
