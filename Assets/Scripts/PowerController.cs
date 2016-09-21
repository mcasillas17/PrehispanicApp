using UnityEngine;
using System.Collections;

public class PowerController : MonoBehaviour {

    public int selected;
    public Sprite[] textures;
    private SpriteRenderer rend;

    void Awake(){
        rend = GetComponent<SpriteRenderer>();
    }
    public void selectRock(){
        selected++; selected %= 2;
        rend.sprite = textures[selected];
    }
    public void deselect(){
        selected = 0;
        rend.sprite = textures[selected];
    }
}
