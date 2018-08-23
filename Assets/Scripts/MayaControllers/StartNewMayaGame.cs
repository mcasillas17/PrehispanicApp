using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartNewMayaGame : MonoBehaviour {

    public AudioSource aud;
    void Awake(){
        aud = Camera.main.GetComponent<AudioSource>();
    }
	void OnMouseDown () {
        aud.Stop();
		SceneManager.LoadScene("SeleccionPeriodo");
    }
}
