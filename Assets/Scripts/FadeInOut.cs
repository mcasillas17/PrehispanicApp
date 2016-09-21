using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {

	public void LoadNextLevel(string name) {
        StartCoroutine(LevelLoad(name));
    }

    IEnumerator LevelLoad(string name) {
        yield return new WaitForSeconds(1f);
        Application.LoadLevel(name);
    }
}
