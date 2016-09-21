using UnityEngine;
using System.Collections;

public class StartNewMayaGame : MonoBehaviour {

    public AudioSource aud;
    public GameObject pan;
    void Awake(){
        aud = Camera.main.GetComponent<AudioSource>();
    }
	void OnMouseDown () {
        aud.Stop();
        pan.GetComponent<Animator>().Play("PanelFadeOut");
        Camera.main.GetComponent<FadeInOut>().LoadNextLevel("SeleccionPeriodo");
    }
}
