using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour {

	public Text text;
	private enum States {
						cell, mirror, sheet_0, lock_0, cell_mirror, sheet_1, lock_1, freedom,
						corridor_0, stairs_0, closet_door, floor, stairs_1, corridor_1, in_closet,
						corridor_2, stairs_2, corridor_3, courtyard
	};
	private States myState;
	private LevelManager levelManager;

	public GameObject destructableObject;

	private bool canTakeMirror;
	private bool hasMirror;
	private bool hasUnlockedDoor;
	private bool isCloseToHairpin;
	private bool isCloseToCloset;
	private bool hasHairpin;
	private bool hasOpenedCloset;
//	public GameObject textGameObject;

	private AudioSource pickUpItemAudio; 

	// Use this for initialization
	void Start () {
		//		set initial state for each scene (scene 01 takes the first in the enum States):
		if (SceneManager.GetActiveScene().buildIndex == 2){
			print ("scene index: " + SceneManager.GetActiveScene ().buildIndex);
			myState = States.corridor_0;
		}
		if (SceneManager.GetActiveScene().buildIndex == 3){
			print ("scene index: " + SceneManager.GetActiveScene ().buildIndex);
			myState = States.in_closet;
		}
		if (SceneManager.GetActiveScene().buildIndex == 4){
			print ("scene index: " + SceneManager.GetActiveScene ().buildIndex);
			myState = States.corridor_3;
		}
		if (SceneManager.GetActiveScene().buildIndex == 5){
			print ("scene index: " + SceneManager.GetActiveScene ().buildIndex);
			myState = States.freedom;
		}

		pickUpItemAudio = GetComponent<AudioSource>(); 
		print("state in start: " + myState);
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
		text.text = "Freedom is a state of mind.";

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
		if (myState == States.cell){
			myState = States.sheet_0;
			print (myState);
			canTakeMirror = false;
		}
		else if(myState == States.cell_mirror){
			myState = States.sheet_1;
			print (myState);
		}

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
		canTakeMirror = false;
	}
	public void CellViewMirror(){
		if(canTakeMirror == true){
			myState = States.cell_mirror;
			print (myState);
			hasMirror = true;
			pickUpItemAudio.Play ();
			Destroy(destructableObject, 0.7f);
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
		text.text = "I'm not sure I'll be able to enjoy a confortable bed " +
			"when I get out. The pleasures of prison life " +
			"I guess!";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.cell;
			print (myState);
		}					
	}

	void sheet_1(){
		text.text = "Holding a mirror in your hand doesn't make " +
			"your bed any more confortable.";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.cell_mirror;
			print (myState);
		}					
	}

	void mirror(){
		text.text = "You hair looks amazing in the mirror " +
			"but now it's not the time to be looking " + 
			"at yourself.";

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
		text.text = "Let's see what I can do with this mirror.";

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
		text.text = "This is a code lock. If I could see the buttons maybe I could " +
			"distinguish fingerprints. Then I just need to try a few combinations " +
			"and get to the rigth one on time. Hopefully.";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.cell;
			print (myState);
		}					
	}

	void lock_1(){
		text.text = "I can can just make out fingerprints around " +
			" the buttons. Now just give it a few tries to get the right " +
			"combination...";

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
			"right. You hear people talking on the top of the stairs.";

		if (Input.GetKeyDown(KeyCode.S)){
			CorridorViewStairs ();
		}
		if (Input.GetKeyDown(KeyCode.F)){
			CorridorViewFloor ();
		}					
		if (Input.GetKeyDown(KeyCode.C)){
			CorridorViewCloset ();
		}					

	}

	public void CorridorViewStairs(){
		if (myState == States.corridor_3){
			myState = States.freedom;
			print (myState);
			levelManager.LoadNextLevel ();
		}
		else if (myState == States.corridor_2){
			myState = States.corridor_3;
			print (myState);
			levelManager.LoadNextLevel ();

		}
		else if (myState == States.in_closet || myState == States.stairs_2){
			myState = States.stairs_2;
			print (myState);

		}
		else if(hasOpenedCloset){
			myState = States.stairs_2;
			print (myState);
		}
		else if(hasHairpin){
			myState = States.stairs_1;
			print (myState);
		}
		else {
			myState = States.stairs_0;
			print (myState);
		}

	}
	public void CorridorViewFloor(){
		if(myState == States.floor){
			myState = States.corridor_1;
			print (myState);
			hasHairpin = true;
			pickUpItemAudio.Play ();
			Destroy(destructableObject);
		}
		else {
			myState = States.floor;
			print (myState);
			isCloseToHairpin = true;
		}
	}
	public void CorridorViewCloset(){
		if(myState == States.in_closet || myState == States.stairs_2 ){
			myState = States.corridor_2;
			print (myState);
			pickUpItemAudio.Play ();
			Destroy(destructableObject);


		}
		else if (myState == States.corridor_1 || myState == States.stairs_1){
			myState = States.in_closet;
			print (myState);
			hasOpenedCloset = true;
			levelManager.LoadNextLevel ();

		}
		else if (myState == States.corridor_0){
			myState = States.closet_door;
			print (myState);
			isCloseToCloset = true;
		}
		else {
			myState = States.closet_door;
			print (myState);
		}
	}

	public void CorridorReturnToCenter(){
		if(hasOpenedCloset){
			myState = States.corridor_2;
			print (myState);
		}
		else {
			myState = States.corridor_0;
			print (myState);
		}
		isCloseToHairpin = false;
		isCloseToCloset = false;

	}

	void stairs_0(){
		text.text = "You get closer to the stairs to try and recognize the voices but " +
			"the sound is too far away, you can't distinguish any words. It's probably "+
			"guards and they are always armed. You decide not to risk it.";

		if (Input.GetKeyDown(KeyCode.R)){
			CorridorReturnToCenter ();
		}					
	}

	void closet_door(){
		text.text = "It's locked. It looks pretty easy to pick, if you " +
			"only had your tools...";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.corridor_0;
			print (myState);
		}					
	}

	void floor(){
		text.text = "What's this here on the floor? Oh, " + 
			"it's just a hairpin.";

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
			"be getting closer, you better take some action fast!";

		if (Input.GetKeyDown(KeyCode.S)){
			myState = States.stairs_1;
			print (myState);
		}					
		if (Input.GetKeyDown(KeyCode.C)){
			CorridorViewCloset ();

		}					
	}

	void stairs_1(){
		text.text = "It's too risky, they're right here.";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.corridor_1;
			print (myState);
		}					
	}
		
	void in_closet(){
		text.text = "The hairpin is perfect for this lock. You pick it and open the closet door. "+
			" There's a cleaners uniform inside and nothing else.";

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
		text.text = "You can ear the guards now, they're getting down the stairs.";

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
			"at the top of the stairs. They will come down in few seconds and find you.";

		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.corridor_2;
			print (myState);
		}					
	}

	void corridor_3(){
		text.text = "They probably won't recognize you wearing this, afterall " +
			"your old cell mate use to call you the janitor. That bastard he's " +
			"been out for 12 years now. He probably got married and had...\n" +
			"Stop daydreaming! They would have found your empty cell by now!";

		if (Input.GetKeyDown(KeyCode.S)){
			myState = States.freedom;
			print (myState);
			levelManager.LoadNextLevel ();
		}					
	}

	void freedom(){
		text.text = "Thank you for playing!\n\n" +
			"by Diogo Farias Rodrigues";

		if (Input.GetKeyDown(KeyCode.P)){
			myState = States.cell;
			print (myState);
		}					
	}
	#endregion
}