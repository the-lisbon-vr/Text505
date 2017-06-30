using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

//	public Image black;
	public GameObject sceneFader;
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
		SpriteRenderer black = sceneFader.GetComponent<SpriteRenderer>();
//		yield return new WaitUntil (() => black.color == new Color32 (255, 255, 255, 1));
		yield return new WaitUntil (() => black.color.a == 1);
//		print ("sprite color: " + black.color.a);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	IEnumerator FadingToScene(string name){
		animator.SetBool ("Fade", true);
		SpriteRenderer black = sceneFader.GetComponent<SpriteRenderer>();
		yield return new WaitUntil (() => black.color.a == 1);
		SceneManager.LoadScene(name);
	}
}
