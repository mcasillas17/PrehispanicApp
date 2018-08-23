using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PeriodController : MonoBehaviour {

    public Text txtPeriod;
    public Button btnNext;
    public Button btnPrevious;

	public GameObject aboutFrame;
    public GameObject map;
	public Sprite[] mapSprites;
    public GameObject[] periods;
    public GameObject[] infoPeriods;
	public AboutUsController auc;
    string[] periodNames = new string[] { "Preclásico", "Clásico", "Posclásico" };
    int currentPeriod;
    bool infoActive = false;

    private void Start () {
        currentPeriod = PlayerPrefs.GetInt("CurrentPeriod",0);
		map.GetComponent<SpriteRenderer> ().sprite = mapSprites[currentPeriod];
		auc = txtPeriod.GetComponent<AboutUsController> ();
        setValues();
	}

    private void setValues(){
        txtPeriod.text = periodNames[currentPeriod];
        for(int i = 0; i < periods.Length; i++){
            periods[i].SetActive(false);
        }periods[currentPeriod].SetActive(true);
    }

    private void Update() {
        checkButtonState();
    }

    //Next & Previous buttons state
    public void checkButtonState() {
        if (currentPeriod == 0) {
            btnPrevious.interactable = false;
        }
        else if (currentPeriod == 2)
            btnNext.interactable = false;
        else {
            btnNext.interactable = true;
            btnPrevious.interactable = true;
        }
    }

	public void changePeriod(int increment){
		aboutFrame.SetActive (false);
		auc.isShowing = false;
        currentPeriod += increment;
        if (currentPeriod >= 0 && currentPeriod < periodNames.Length) setValues();
        if (currentPeriod < 0) currentPeriod = 0;
        if (currentPeriod >= periodNames.Length) currentPeriod = periodNames.Length - 1;
        PlayerPrefs.SetInt("CurrentPeriod",currentPeriod);
		map.GetComponent<SpriteRenderer> ().sprite = mapSprites[currentPeriod];
    }

    //For showing Period information
    public void setInfoDialog(GameObject selectedObject) {
        if (selectedObject.tag.Equals("btnInfo")) {
            infoActive = !infoActive;
            infoPeriods[currentPeriod].SetActive(infoActive);
        } else {
            for (int i = 0; i < infoPeriods.Length; i++) {
                infoPeriods[i].SetActive(false);
                infoActive = false;
            }
        }
    }

}
