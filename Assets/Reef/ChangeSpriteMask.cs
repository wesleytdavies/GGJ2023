using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteMask : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;


    private IEnumerator Switch(){
        while(true){
            yield return new WaitForSeconds(0.25f);
            ChangeSprite();
        }
        
    }
    void Start(){
        StartCoroutine(Switch());
    }
    void ChangeSprite()
    {
        if(spriteRenderer.sprite == sprite1){
            spriteRenderer.sprite = sprite2;
        } else if (spriteRenderer.sprite == sprite2){
            spriteRenderer.sprite = sprite3;
        } else if (spriteRenderer.sprite = sprite3){
            spriteRenderer.sprite = sprite1;
        }
    }
}
