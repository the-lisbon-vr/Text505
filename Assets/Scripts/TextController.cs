using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	public Text text;
	private enum States {
						cell, mirror, sheet_0, lock_0, cell_mirror, sheet_1, lock_1, freedom,
						corridor_0, stairs_0, closet_door, floor, stairs_1, corridor_1, in_closet,
						corridor_2, stairs_2, corridor_3, courtyard
	};
	private States myState;
	private LevelManager levelManager;

	private bool canTakeMirror;
	private bool hasMirror;
	private bool hasUnlockedDoor;
//	public GameObject textGameObject;

	// Use this for initialization
	void Start () {
		myState = States.cell;
		print(myState);
		levelManager = Object.FindObjectOfType<LevelManager> ();
	}

	// Update is called once per frame
	void Update () {
		if (myState == States.cell){
			cell();
		}
		else if (myState == States.sheet_0){
			sheet_0();
		}
		else if (myState == States.lock_0){
			lock_0();
		}
		else if (myState == States.mirror){
			mirror();
		}
		else if (myState == States.cell_mirror){
			cell_mirror();
		}
		else if (myState == States.sheet_1){
			sheet_1();
		}
		else if (myState == States.lock_1){
			lock_1();
		}
		else if (myState == States.corridor_0){
			corridor_0();
		}
		else if (myState == States.corridor_1){
			corridor_1();
		}
		else if (myState == States.stairs_0){
			stairs_0();
		}
		else if (myState == States.stairs_1){
			stairs_1();
		}
		else if (myState == States.closet_door){
			closet_door();
		}
		else if (myState == States.floor){
			floor();
		}
		else if (myState == States.in_closet){
			in_closet();
		}
		else if (myState == States.corridor_2){
			corridor_2();
		}
		else if (myState == States.stairs_2){
			stairs_2();
		}
		else if (myState == States.corridor_3){
			corridor_3();
		}
		else if (myState == States.freedom){
			freedom();
		}



	}

	#region State handler methods
	void cell(){
		text.text = "You are in a prison cell, you want to escape. There are " +
			"some dirt sheets on the bed, a mirror on the wall and the " +
			"door is locked from the outside.\n\n" +
			"Press S to view sheets, M to view mirror and L to view lock.";

		if (Input.GetKeyDown(KeyCode.S)){
			myState = States.sheet_0;
			print (myState);
		}						
		if (Input.GetKeyDown(KeyCode.L)){
			myState = States.lock_0;
			print (myState);
		}						
		if (Input.GetKeyDown(KeyCode.M)){
			myState = States.mirror;
			print (myState);
		}						
	}

	public void CellViewSheets(){
		myState = States.sheet_0;
		print (myState);
	}
	public void CellViewLock(){
		if(hasUnlockedDoor == true){
			myState = States.corridor_0;
			print (myState);
			levelManager.LoadNextLevel ();
		}
		else if (hasMirror == true){
			myState = States.lock_1;
			print (myState);
			hasUnlockedDoor = true;
		} else {
			myState = States.lock_0;
			print (myState);
		}
	}
	public void CellViewMirror(){
		if(canTakeMirror == true){
			myState = States.cell_mirror;
			print (myState);
			hasMirror = true;
		}
		else {
			myState = States.mirror;
			print (myState);
			canTakeMirror = true;
		}
	}
	public void CellReturnToCenter(){
		myState = States.cell;
		print (myState);
	}


	void sheet_0(){
		text.text = "You can't believe you sleep in these things. Surely it's " +
			"time somebody changed them. The pleasures of prison life " +
			"I guess!\n\n" +
			"Press R to return to roaming you cell.";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.cell;
			print (myState);
		}					
	}

	void sheet_1(){
		text.text = "Holding a mirror in your hand doesn't make " +
			"your sheets look any better.\n\n" +
			"Press R to return to roaming you cell.";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.cell_mirror;
			print (myState);
		}					
	}

	void mirror(){
		text.text = "You whipe the dust off of the mirror. Your hair looks " +
			"amazing today but now it's not the time to be looking " + 
			"at yourself.\n\n" +
			"Press T to take the mirror with you or R to return to roaming you cell.";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.cell;
			print (myState);
		}					
		if (Input.GetKeyDown(KeyCode.T)){
			myState = States.cell_mirror;
			print (myState);
			hasMirror = true;
		}					
	}

	void cell_mirror(){
		text.text = "You've taken the mirror but now what? Try to think of a " +
			"solution. Maybe you can use it to cut the sheets or...\n\n" +
			"Press S to view sheets or L to view lock.";

		if (Input.GetKeyDown(KeyCode.S)){
			myState = States.sheet_1;
			print (myState);
		}					
		if (Input.GetKeyDown(KeyCode.L)){
			myState = States.lock_1;
			print (myState);
		}					
	}

	void lock_0(){
		text.text = "This is one of those button locks. You have no idea what the " +
			"combination is. You wish you could somehow see where the dirty " +
			"fingerprints were, maybe that would help.\n\n" +
			"Press R to return to roaming you cell.";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.cell;
			print (myState);
		}					
	}

	void lock_1(){
		text.text = "You carefully put the mirror between the bars and turn it round " +
			"so you can see the lock. You can just make out fingerprints around " +
			" the buttons. You press the dirty buttons and hear a click.\n\n" +
			"Press O to open or R to return to roaming you cell.";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.cell_mirror;
			print (myState);
		}					
		else if (Input.GetKeyDown(KeyCode.O)){
			myState = States.corridor_0;
			print (myState);
			levelManager.LoadNextLevel ();
		}					
	}

	void corridor_0(){
		text.text = "You are in a corridor. There are stairs to your left, " +
			" there's something on the floor and there's a closet to your " +
			"right. You hear people talking on the top of the stairs.\n\n" +
			"Press S to view stairs, F to view floor and C to view closet.";

		if (Input.GetKeyDown(KeyCode.S)){
			myState = States.stairs_0;
			print (myState);
		}
		if (Input.GetKeyDown(KeyCode.F)){
			myState = States.floor;
			print (myState);
		}					
		if (Input.GetKeyDown(KeyCode.C)){
			myState = States.closet_door;
			print (myState);
		}					

	}

	void stairs_0(){
		text.text = "You get closer to the stairs to try and recognize the voices but " +
			"the sound is too far away, you can't distinguish any words. It's probably "+
			"guards and they are always armed. You decide not to risk it.\n\n" +
			"Press R to return.";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.corridor_0;
			print (myState);
		}					
	}

	void closet_door(){
		text.text = "It's locked. It looks pretty easy to pick, if you " +
			"only had your tools...\n\n" +
			"Press R to return to roaming the corridor.";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.corridor_0;
			print (myState);
		}					
	}

	void floor(){
		text.text = "What's this here on the floor? Oh, " + 
			"it's just a hairpin.\n\n" +
			"Press H to take the hairpin or R to return.";

		if (Input.GetKeyDown(KeyCode.H)){
			myState = States.corridor_1;
			print (myState);
		}					
		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.corridor_0;
			print (myState);
		}					
	}

	void corridor_1(){
		text.text = "The chatter coming from the top of the stairs seems to " + 
			"be getting closer, you better take some action fast!\n\n" +
			"Press S to view the stairs or C to view the closet.";

		if (Input.GetKeyDown(KeyCode.S)){
			myState = States.stairs_1;
			print (myState);
		}					
		if (Input.GetKeyDown(KeyCode.C)){
			myState = States.in_closet;
			print (myState);
			levelManager.LoadNextLevel ();

		}					
	}

	void stairs_1(){
		text.text = "It's too risky, they're right here.\n\n" +
			"Press R to return to roaming the corridor";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.corridor_1;
			print (myState);
		}					
	}
		
	void in_closet(){
		text.text = "The hairpin is perfect for this lock. You pick it and open the closet door. "+
			" There's a cleaners uniform inside and nothing else.\n\n" +
			"Press R to return to roaming the corridor or press D to dress the uniform.";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.corridor_2;
			print (myState);
		}					
		if (Input.GetKeyDown(KeyCode.D)){
			myState = States.corridor_3;
			print (myState);
		}					
	}

	void corridor_2(){
		text.text = "You can ear the guards now, they're getting down the stairs.\n\n" +
			"Press S to view the stairs or C to view the closet.";

		if (Input.GetKeyDown(KeyCode.S)){
			myState = States.stairs_2;
			print (myState);
		}					
		if (Input.GetKeyDown(KeyCode.C)){
			myState = States.in_closet;
			print (myState);
		}					
	}

	void stairs_2(){
		text.text = "You peek around and you can see them talking, just standing, " +
			"at the top of the stairs. They will come down in few seconds and find you.\n\n" +
			"Press R to return to roaming the corridor";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.corridor_2;
			print (myState);
		}					
	}

	void corridor_3(){
		text.text = "They probably won't recognize you wearing this, afterall " +
			"your old cell mate use to call you the janitor. That bastard he's " +
			"been out for 12 years now. He probably got married and had...\n" +
			"Stop daydreaming! They would have found your empty cell by now!\n\n" +
			"Press S to view the stairs.";

		if (Input.GetKeyDown(KeyCode.S)){
			myState = States.freedom;
			print (myState);
			levelManager.LoadNextLevel ();
		}					
	}

	void freedom(){
		text.text = "You're free! Now run!\n\n" +
			"Press P to play again.";

		if (Input.GetKeyDown(KeyCode.P)){
			myState = States.cell;
			print (myState);
		}					
	}
	#endregion
}