using UnityEngine;
using System.Collections;

public class AboutUsController : MonoBehaviour {

	public bool isShowing = false;
	public GameObject aboutFrame;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void setAboutUs() {
		isShowing = !isShowing;
		aboutFrame.SetActive (isShowing);
	}
}
