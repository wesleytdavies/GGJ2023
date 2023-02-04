using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBoil : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    public float spriteChange; 
    public Sprite spriteOne;
    public Sprite spriteTwo;
    public Sprite spriteThree;
    public Sprite spriteFour;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>(); //getting my sprite renderer
    }

    void ChangeSprite()
    {
        //myRenderer.sprite = otherSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
