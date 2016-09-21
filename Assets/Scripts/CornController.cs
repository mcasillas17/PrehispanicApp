using UnityEngine;
using System.Collections;

public class CornController : MonoBehaviour {
	
	/* 0 - sano
	   1 - brote
	   2 - quemado
	   3 - gusanos
	   4 - ahogado
	   5 - vacio*/
	public int state;
	public int selected;
	public Sprite[] defaultSprites;
	public Sprite[] selectedSprites;
	public SpriteRenderer rend;

	void Awake(){
		rend = GetComponent<SpriteRenderer> ();
	}
	public void changeSprite(){
		if(selected==0)
			rend.sprite = defaultSprites [state];
		else rend.sprite = selectedSprites [state];
	}
	public void setCornState(int st){
		state = st; changeSprite ();
	}

	public void selectCorn(){
		selected++; selected %= 2;
		changeSprite ();
	}
}
