using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus2DPlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D _rb;
    Vector2 _movement;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        moveCharacter(_movement);
    }

    void moveCharacter(Vector2 direction)
    {
        _rb.velocity = (direction * speed);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Patroler")
        {
            Destroy(gameObject);
        }
    }
}
