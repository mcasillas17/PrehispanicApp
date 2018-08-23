using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSelectionController : MonoBehaviour {

	string[] btnNames = new string[]{"btnOlmeca","btnMaya","btnMexica"};
	string[] sceneNames = new string[]{"OlmecaGame","MayaGame","MexicaGame"};
    void OnMouseDown(){
		int selected = 0;
		for (; selected<btnNames.Length; selected++) {
			if(this.tag == btnNames[selected]) break;
		}
		SceneManager.LoadScene(sceneNames[selected]);
    }
}
