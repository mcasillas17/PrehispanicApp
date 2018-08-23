using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MayaNumberController : MonoBehaviour {
	public Button btnNumber; //MayaNumber Button
	public Text totalNumber; //Text that has the total number
	public int mayaNumber=5, arabicNumber; // the maya and the arabic numbers
	private MayaGameController mgc; //The MayaGameController component
	public Camera cam; //The camera of the Game (set the background music here)
	public GameObject [] masks; //Masks objects
	private int lastMaskIndex; //The mask we are referencing (maps to lifes)
	public Sprite [] numbers; //The sprite numbers
    public GameObject[] mensajes;

	void Awake() {
        //Get the maya game controller
		mgc = cam.GetComponent<MayaGameController>();
        //set the initial arabic number
        arabicNumber = Random.Range (17, 39);
        totalNumber.text = "" + arabicNumber;
        //Set the lastMaskIndex into the last maks in the array
        lastMaskIndex = masks.Length - 1;
        //Put the corresponding image into btnNumber
        int n = Random.Range(1, 11); //create an initial random [1,10]
                                     //create a new number if it is equal to the previous one
                                     // or is greater to the arabic number showing
        while (n == mayaNumber || n > arabicNumber)
        {
            //if the arabic number is less than 10 create a random [1,arabic]
            if (arabicNumber < 10) n = Random.Range(1, arabicNumber + 1);
            else n = Random.Range(1, 11); //or [1,10]
        }
        mayaNumber = n; //put the n into mayaNumber
        btnNumber.image.overrideSprite = numbers[mayaNumber]; //change the image to the Button
    }
    //We lost the game
	private void loseMayaGame(){
        mensajes[1].SetActive(true);
	}
    //We won the game
    private void winMayaGame(){
        mensajes[0].SetActive(true);
		mgc.isPlaying = false;
    }
    //Check if the number of selected corns are equal to the
    //showing maya number
    private int checkCorrectCorns(){
        //Get the rows and columns in the grid of corns
		int rows = mgc.cornRows, cols = mgc.cornCols;
        //Count of selected corns
		int count = 0;
        //Corn grid
		GameObject [,] corns = mgc.cornObjects;
        //Iterate in the corn grid
		for (int i=0; i<rows; i++) {
			for(int j=0;j<cols;j++){
                //Get the controller for the [i,j] corn
				CornController cornCont = corns[i,j].GetComponent<CornController>();
                //If the corn is selected and the corn state is healthy
				if(cornCont.selected == 1 && cornCont.state == 0){
					cornCont.setCornState(Random.Range(1,5)); //change the corn state
					count++; //increment the count of selected corns
                }
                cornCont.selected = 0; //deselect the corn
                cornCont.changeSprite(); //change the corn sprite
            }
		}
        return count;
	}
    //Returns true if it is a valid move
    private bool checkValidMove(int correctCorns){ 
        //Check if is the correct number in maya
        if (correctCorns == mayaNumber){
            arabicNumber -= correctCorns; //decrease the arabic number
            totalNumber.text = "" + arabicNumber; //change the number in the text
            if (arabicNumber == 0){ //Means you won
                winMayaGame();
                return false;
            }
        }else{
            if (lastMaskIndex >= 0){ //Means you are in a valid mask index (you have lifes remaining)
                masks[lastMaskIndex--].SetActive(false); //we are gonna fadeout the mask
                if (lastMaskIndex < 0) loseMayaGame();
            }else{
                return false;
            }
        }
        return true;
    }

	public void generateNewMayaNumber(){
		if (lastMaskIndex >= 0 && arabicNumber>0 && !mgc.showInstructions){
            //Save the correctCorns in the grid
            int correctCorns = checkCorrectCorns();
            if (checkValidMove(correctCorns)){ //If it is a valid move we need to create a new maya number
                int n = Random.Range(1, 11); //create an initial random [1,10]
                                             //create a new number if it is equal to the previous one
                                             // or is greater to the arabic number showing
				if (arabicNumber > 2) {
					while (n == mayaNumber || n > arabicNumber) {
						//if the arabic number is less than 10 create a random [1,arabic]
						if (arabicNumber < 10)
							n = Random.Range (1, arabicNumber + 1);
						else
							n = Random.Range (1, 11); //or [1,10]
					}
				} else
					n = 1;
                mayaNumber = n; //put the n into mayaNumber
                btnNumber.image.overrideSprite = numbers[mayaNumber]; //change the image to the Button
            }
        }
	}
}
