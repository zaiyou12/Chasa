using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		IEnumerator coroutine = WaitForStart ();
		StartCoroutine (coroutine);
	}
	
	IEnumerator WaitForStart(){
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene(1);
	}
}
