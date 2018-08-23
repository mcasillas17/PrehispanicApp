using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MayaGameController : MonoBehaviour {

    public GameObject corn;
    public int cornCols, cornRows;
    public GameObject[,] cornObjects;
    public GameObject[] rocks;
    public GameObject[] powers;
    public bool isPowerSelected;
    public int selectedPower;

    public bool showInstructions;
	public bool showExit=false;
    public int currentInstruction;
	public GameObject teco;
	public GameObject exitFrame;
	public GameObject[] fila1,fila2,fila3,fila4;

	public bool isPlaying = false;

	void initCornGrid2(){
		for (int i = 0; i < cornCols; i++) {
			cornObjects [0, i] = fila1 [i];
			cornObjects [1, i] = fila2 [i];
			cornObjects [2, i] = fila3 [i];
			cornObjects [3, i] = fila4 [i];
		}
		int N = cornRows * cornCols, st;
		for (int i=0; i<cornRows; i++) {
			for (int j=0; j<cornCols; j++) {
				int value = Random.Range(0,N);
				if(value<N/2) st = 0;
				else if(value<5*N/8) st = 1;
				else if(value<6*N/8) st = 2;
				else if(value<7*N/8) st = 3;
				else st = 4;
				CornController cornScript = cornObjects [i, j].GetComponent<CornController> ();
				cornScript.setCornState(st);
			}
		}
	}

	// Use this for initialization
	void Awake () {
		cornObjects = new GameObject[cornRows, cornCols];
		initCornGrid2();
		corn = cornObjects [0, 0];
        isPowerSelected = false;
        selectedPower = -1;
        showInstructions = true;
		currentInstruction = 0;
	}

    void Start() {
        //Check if it's the first time the game is played
        //in order to show instructions
		isPlaying = false;
		StartCoroutine(applyTecolote());
    }


    //Check if we touched a corn on the grid
    // returns the i,j indexes in a vector 2
    // Vector2.zero if we didn't touch a corn
    private Vector2 checkSelectCorn(Vector2 inputPosition){
        for (int i = 0; i < cornRows; i++){
            for (int j = 0; j < cornCols; j++){
                Ray ray = Camera.main.ScreenPointToRay(inputPosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                //check the hit
                if (hit.collider != null && hit.collider.transform == cornObjects[i, j].transform){
                    return new Vector2(i, j);
                }
            }
        }
        return new Vector2(-1, -1);
    }

    private void deselectAllCorns(){
        for (int i = 0; i < cornRows; i++)
        {
            for (int j = 0; j < cornCols; j++)
            {
                CornController cc = cornObjects[i, j].GetComponent<CornController>();
                cc.selected = 0; cc.changeSprite();
            }
        }

    }

    private int checkSelectRock(Vector2 inputPosition){
        for (int j = 0; j < 4; j++){
            Ray ray = Camera.main.ScreenPointToRay(inputPosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            //check the hit
            if (hit.collider != null && hit.collider.transform == rocks[j].transform){
                return j;
            }
        }
        return -1;
    }

    private void changeOneCornState(CornController cornContr){
        if (cornContr.state == 0){//State = "sano"
            if (selectedPower == 0)//Selected power => moon
                cornContr.state = 3;//new state = "gusanos"
            else if (selectedPower == 1)//power => rain
                cornContr.state = 4;//new state = "ahogado"
            else if (selectedPower == 3)//power => sun
                cornContr.state = 2; // new state = "quemado"
        }else if (cornContr.state == 1){//State = "brote"
            if (selectedPower == 0)//Selected power => moon
                cornContr.state = 3;//new state = "gusanos"
            else if (selectedPower == 1)//power => rain
                cornContr.state = 0;//new state = "ahogado"
            else if (selectedPower == 2)//power => mountain
                cornContr.state = 0; // new state = "quemado"
            else if (selectedPower == 3)//power => sun
                cornContr.state = 5; // new state = "vacio"
        }else if (cornContr.state == 2){//State = "quemado"
            if (selectedPower == 0)//Selected power => moon
                cornContr.state = 0;//new state = "sano"
            else if (selectedPower == 1)//power => rain
                cornContr.state = 0;//new state = "sano"
            else if (selectedPower == 3)//power => sun
                cornContr.state = 5; // new state = "vacio"
        }else if (cornContr.state == 3){//State = "gusanos"
            if (selectedPower == 0)//Selected power => moon
                cornContr.state = 5;//new state = "vacio"
            else if (selectedPower == 2)//power => mountain
                cornContr.state = 0; // new state = "sano"
            else if (selectedPower == 3)//power => sun
                cornContr.state = 0; // new state = "sano"
        }else if (cornContr.state == 4){//State = "ahogado"
            if (selectedPower == 0)//Selected power => moon
                cornContr.state = 0;//new state = "sano"
            else if (selectedPower == 1)//power => rain
                cornContr.state = 5;//new state = "vacio"
            else if (selectedPower == 2)//power => mountain
                cornContr.state = 5; // new state = "vacio"
            else if (selectedPower == 3)//power => sun
                cornContr.state = 0; // new state = "sano"
        }else if (cornContr.state == 5){//State = "vacio"
            if (selectedPower == 1)//power => rain
                cornContr.state = 1;//new state = "brote"
        }
        cornContr.changeSprite();
    }

    private void applyMoonPower(Vector2 cornCoords){
        int x = (int)cornCoords.x, y = (int)cornCoords.y;
        int[] r = {0,0,1,1};
        int[] c = {0,1,1,0};
        for (int i = 0; i < 4; i++){
            if(x+r[i]>=0 && x+r[i]<4 && y+c[i]>=0 && y + c[i] < 5){ //We can instantiate a power
                Vector2 pos = cornObjects[x + r[i], y + c[i]].transform.position;
                GameObject temp = (GameObject)Instantiate(powers[0], new Vector3(pos.x, pos.y, powers[0].transform.position.z), Quaternion.identity);
                Object.Destroy(temp, 1.2f);
                changeOneCornState(cornObjects[x + r[i], y + c[i]].GetComponent <CornController>());
            }
        }
    }
    private void applyRainPower(Vector2 cornCoords){
        int x = (int)cornCoords.x, y = (int)cornCoords.y;
        int[] r = { 0, 0, 1, 2 };
        int[] c = { 0, -1, 0, 0 };
        for (int i = 0; i < 4; i++)
        {
            if (x + r[i] >= 0 && x + r[i] < 4 && y + c[i] >= 0 && y + c[i] < 5)
            { //We can instantiate a power
                Vector2 pos = cornObjects[x + r[i], y + c[i]].transform.position;
                GameObject temp = (GameObject)Instantiate(powers[1], new Vector3(pos.x, pos.y, powers[1].transform.position.z), Quaternion.identity);
                Object.Destroy(temp, 1.2f);
                changeOneCornState(cornObjects[x + r[i], y + c[i]].GetComponent<CornController>());
            }
        }
    }
    private void applyMountainPower(Vector2 cornCoords){
        int x = (int)cornCoords.x, y = (int)cornCoords.y;
        int[] r = { 0, 0, 0, -1 };
        int[] c = { 0, 1, -1, 0 };
        for (int i = 0; i < 4; i++)
        {
            if (x + r[i] >= 0 && x + r[i] < 4 && y + c[i] >= 0 && y + c[i] < 5)
            { //We can instantiate a power
                Vector2 pos = cornObjects[x + r[i], y + c[i]].transform.position;
                GameObject temp = (GameObject)Instantiate(powers[2], new Vector3(pos.x, pos.y, powers[2].transform.position.z), Quaternion.identity);
                Object.Destroy(temp, 1.2f);
                changeOneCornState(cornObjects[x + r[i], y + c[i]].GetComponent<CornController>());
            }
        }
    }
    private void applySunPower(Vector2 cornCoords){
        int x = (int)cornCoords.x, y = (int)cornCoords.y;
        int[] r = { 0, 0, 0, -1, 1 };
        int[] c = { 0, 1, -1, 0, 0 };
        for (int i = 0; i < 5; i++)
        {
            if (x + r[i] >= 0 && x + r[i] < 4 && y + c[i] >= 0 && y + c[i] < 5)
            { //We can instantiate a power
                Vector2 pos = cornObjects[x + r[i], y + c[i]].transform.position;
                GameObject temp = (GameObject)Instantiate(powers[3], new Vector3(pos.x, pos.y, powers[3].transform.position.z), Quaternion.identity);
                Object.Destroy(temp, 1.2f);
                changeOneCornState(cornObjects[x + r[i], y + c[i]].GetComponent<CornController>());
            }
        }
    }
    private void applyPowerToCorns(Vector2 cornCoords){
        if (selectedPower == 0) applyMoonPower(cornCoords);
        else if (selectedPower == 1) applyRainPower(cornCoords);
        else if (selectedPower == 2) applyMountainPower(cornCoords);
        else applySunPower(cornCoords);
    }

	private void changeCornStates(){
		deselectAllCorns();
		List<CornController> corn_list = new List<CornController>();
		for (int i=0; i<cornRows; i++) {
			for (int j=0; j<cornCols; j++) {
				CornController cornScript = cornObjects [i, j].GetComponent<CornController> ();
				if (cornScript.state == 0) {
					corn_list.Add (cornScript);
				}
			}
		}
		for (int i = 0; i < corn_list.Count; i++) {
			CornController temp = corn_list [i];
			int index = Random.Range (i, corn_list.Count);
			corn_list [i] = corn_list [index];
			corn_list [index] = temp;
		}
		for (int i = 0; i < corn_list.Count / 2; i++) {
			corn_list[i].setCornState(Random.Range(1,4));
		}
	}

	IEnumerator applyTecolote(){
		while (true) {
			if (isPlaying) {
				//Debug.Log ("isPlaying: "+isPlaying);
				GameObject temp = (GameObject)Instantiate (teco, new Vector3 (0, 0, teco.transform.position.z), Quaternion.identity);
				Object.Destroy (temp, 3.5f);
				Invoke ("changeCornStates", 1.2f);
			}
			yield return new WaitForSeconds (Random.Range (22.0f, 30.0f));
		}
	}

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			exitFrame.SetActive (!showExit);
			showExit = !showExit;
		}
        //Debug.Log("Selected Power: " + selectedPower+ " c: "+(c++));
        Vector2 inputPosition = new Vector2(0,0);
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBPLAYER
        if (Input.GetMouseButtonDown (0)) {
			inputPosition = Input.mousePosition;
            if (!showInstructions && !isPowerSelected){//If there's no power selected
                Vector2 corn = checkSelectCorn(inputPosition);
                if (corn.x >= 0) { //We selected a corn
                    CornController cornCont = cornObjects[(int)corn.x, (int)corn.y].GetComponent<CornController>();
                    cornCont.selectCorn();
                }
                else{
                    int r = checkSelectRock(inputPosition);
                    if (r >= 0){
                        PowerController pw = rocks[r].GetComponent<PowerController>();
                        pw.selectRock();
                        selectedPower = r;
                        isPowerSelected = true;
                    }
                }
            }else{//We have a power selected
                int r = checkSelectRock(inputPosition);
                if (r >= 0){ // we selected a rock
                    if (selectedPower == r){ //we selected the same rock
                        PowerController pw = rocks[r].GetComponent<PowerController>();
                        pw.deselect();
                        selectedPower = -1;
                        isPowerSelected = false;
                    }
                    else{ // we selected a different rock
                        PowerController pw;
                        if (selectedPower >= 0){
                            pw = rocks[selectedPower].GetComponent<PowerController>();
                            pw.deselect(); //Deselect the previous one
                        }
                        pw = rocks[r].GetComponent<PowerController>();
                        pw.selectRock(); //select the new one
                        selectedPower = r;
                        isPowerSelected = true;
                    }
                }else{//we have a power selected
                    Vector2 corn = checkSelectCorn(inputPosition);
                    if (corn.x >=0 ){ //We selected a corn
                        deselectAllCorns();
                        applyPowerToCorns(corn);
                        rocks[selectedPower].GetComponent<PowerController>().deselect();
                        selectedPower = -1;
                        isPowerSelected = false;
                    }
                }
            }
            
        }

#if UNITY_ANDROID
        if (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			inputPosition = Input.GetTouch(0).position;
            if (!isPowerSelected){//If there's no power selected
                Vector2 corn = checkSelectCorn(inputPosition);
                if (corn.x >= 0)
                { //We selected a corn
                    CornController cornCont = cornObjects[(int)corn.x, (int)corn.y].GetComponent<CornController>();
                    cornCont.selectCorn();
                }
                else
                {
                    int r = checkSelectRock(inputPosition);
                    if (r >= 0)
                    {
                        PowerController pw = rocks[r].GetComponent<PowerController>();
                        pw.selectRock();
                        selectedPower = r;
                        isPowerSelected = true;
                    }
                }
            }
            else
            {//We have a power selected
                int r = checkSelectRock(inputPosition);
                if (r >= 0)
                { // we selected a rock
                    if (selectedPower == r)
                    { //we selected the same rock
                        PowerController pw = rocks[r].GetComponent<PowerController>();
                        pw.deselect();
                        selectedPower = -1;
                        isPowerSelected = false;
                    }
                    else
                    { // we selected a different rock
                        PowerController pw;
                        if (selectedPower >= 0)
                        {
                            pw = rocks[selectedPower].GetComponent<PowerController>();
                            pw.deselect(); //Deselect the previous one
                        }
                        pw = rocks[r].GetComponent<PowerController>();
                        pw.selectRock(); //select the new one
                        selectedPower = r;
                        isPowerSelected = true;
                    }
                }
                else
                {//we have a power selected
                    Vector2 corn = checkSelectCorn(inputPosition);
                    if (corn.x >= 0)
                    { //We selected a corn
                        deselectAllCorns();
                        applyPowerToCorns(corn);
                        rocks[selectedPower].GetComponent<PowerController>().deselect();
                        selectedPower = -1;
                        isPowerSelected = false;
                    }
                }
            }
        }
#endif
        
	}


}
