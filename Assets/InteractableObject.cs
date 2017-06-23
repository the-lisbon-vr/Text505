using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

	private LevelManager levelManager;
	private TextController textController;

	// Use this for initialization
	void Start () {
		levelManager = Object.FindObjectOfType<LevelManager> ();
		textController = Object.FindObjectOfType<TextController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame(){
		levelManager.LoadNextLevel ();
	}

	public void CellViewSheets(){
		textController.CellViewSheets ();
	}
	public void CellViewLock(){
		textController.CellViewLock();
	}
	public void CellViewMirror(){
		textController.CellViewMirror ();
	}
}
