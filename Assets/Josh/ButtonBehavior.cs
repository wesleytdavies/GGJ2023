using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{

    public Color basicColor;
    public Color hoverColor;
    public bool quit;
    public string goToSceneName;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = basicColor;
    }

    void OnMouseEnter() //when mouse hovers, color changes
    {
        spriteRenderer.color = hoverColor;
    }

    void OnMouseDown()
    {
        if(spriteRenderer.color == hoverColor){
            Debug.Log("click");
            if(quit == false){
                SceneManager.LoadScene(goToSceneName);
                Debug.Log("nextroom");
            } else {
                Application.Quit();
                Debug.Log("quit");
            }
        }
    }

    void OnMouseExit() // when mouse leaves, color turns white
    {
        spriteRenderer.color = basicColor;
    }

}
