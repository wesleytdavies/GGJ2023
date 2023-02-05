using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus2DPlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D _rb;
    Vector2 _movement;
    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") != 0)
        {
            m_Animator.SetBool("Vertical", false);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            m_Animator.SetBool("Vertical", true);
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(_movement);
    }

    void moveCharacter(Vector2 direction)
    {
        //Debug.Log(direction);
        _rb.velocity = (direction * speed);
    }

    //Kill the player when it collides with Patroler
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Patroler")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Folder")
        {
            Debug.Log("Hit Folder");
        }
    }
}
