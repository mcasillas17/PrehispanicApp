using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoHomeController : MonoBehaviour {

	public void GoHome(){
		SceneManager.LoadScene("SeleccionPeriodo");
	}
}
