using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstructionsController : MonoBehaviour {

	public Button btnNext;
	public GameObject[] instrucciones;
	int currentInstruction;
	private MayaGameController mgc;
	public Camera cam;

	void Awake () {
		for (int i = 1; i < instrucciones.Length; i++)
			instrucciones [i].SetActive (false);
		instrucciones [0].SetActive(true);
		currentInstruction = 0;
		mgc = cam.GetComponent<MayaGameController> ();
	}

	public void nextInstruction() {
		instrucciones [currentInstruction].SetActive (false);
		currentInstruction++;
		if (currentInstruction < instrucciones.Length)
			instrucciones [currentInstruction].SetActive (true);
		else {
			btnNext.enabled = false;
			btnNext.transform.localScale = new Vector3(0,0,0);
			mgc.showInstructions = false;
			mgc.isPlaying = true;
		}
	}
}
