using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public Image black;
	public Animator animator;

	public void LoadLevel(string name){
		StartCoroutine (FadingToScene(name));
	}

	public void QuitGame(){
		print ("Quit game requested");
		Application.Quit();

	}

	public void LoadNextLevel(){
		StartCoroutine (Fading ());

	}

	IEnumerator Fading(){
		animator.SetBool ("Fade", true);
		yield return new WaitUntil (() => black.color.a == 1);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	IEnumerator FadingToScene(string name){
		animator.SetBool ("Fade", true);
		yield return new WaitUntil (() => black.color.a == 1);
		SceneManager.LoadScene(name);
	}
}
