using UnityEngine;
using System.Collections;

public class InitialSceneController : MonoBehaviour {

    public AudioSource audioInitial;
    public void start(){
        audioInitial.Stop();
        //StartCoroutine(TransitionToNewScene());
    }

	public IEnumerator TransitionToNewScene(){
        float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(1.0f);
        Application.LoadLevel("SeleccionPeriodo");
    }
}
