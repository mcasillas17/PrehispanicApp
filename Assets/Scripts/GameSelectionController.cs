using UnityEngine;
using System.Collections;

public class GameSelectionController : MonoBehaviour {

	string[] btnNames = new string[]{"btnOlmeca","btnMaya","btnMexica"};
	string[] sceneNames = new string[]{"OlmecaGame","MayaGame","MexicaGame"};
    public GameObject pan;
    public AudioSource aud;
    void Awake(){
        aud = Camera.main.GetComponent<AudioSource>();
    }
    void OnMouseDown(){
		int selected = 0;
		for (; selected<btnNames.Length; selected++) {
			if(this.tag == btnNames[selected]) break;
		}
        aud.Stop();
        pan.GetComponent<Animator>().Play("PanelFadeOut");
        Camera.main.GetComponent<FadeInOut>().LoadNextLevel(sceneNames[selected]);
        //StartCoroutine(TransitionToNewScene(selected));
    }
    public IEnumerator TransitionToNewScene(int selected)
    {
        float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(1.0f);
        Application.LoadLevel(sceneNames[selected]);
    }
}
