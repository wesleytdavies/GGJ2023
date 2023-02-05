using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    public Vector2 _destination;
    public float _speed = 2;
    public List<Folder> _folders;
    public int randomNumber = 0;
    GameObject _map;
    public bool isAlarmed = false;

    // Start is called before the first frame update
    void Start()
    {
        _map = GameObject.Find("Map");
        _folders = _map.GetComponent<Map>().Folders;
        _destination = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 destination = _destination;
        Vector3 newpos = Vector3.MoveTowards(transform.position, destination, _speed * Time.deltaTime);
        transform.position = newpos;

        float distance = Vector3.Distance(transform.position, destination);
        if (distance <= 0.05 && !isAlarmed)
        {
            _folders = _map.GetComponent<Map>().Folders;
            randomNumber = Random.Range(1, _folders.Count - 1);
            _destination = new Vector2(_folders[randomNumber].gameObject.transform.position.x, _folders[randomNumber].gameObject.transform.position.y);
        }

        if(isAlarmed)
        {
            //isAlarmed = false;
            _destination = GameObject.Find("VirusPlayer").transform.position;
        }
    }

}
