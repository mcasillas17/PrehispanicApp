using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InitialSceneController : MonoBehaviour {

    public AudioSource audioInitial;
    public void start(){
        audioInitial.Stop();
        //StartCoroutine(TransitionToNewScene());
    }
	public void moveToSelection(){
		SceneManager.LoadScene("SeleccionPeriodo");
	}
}
