using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneManager : MonoBehaviour {

	public CameraMove cameraMove;
	public CubeMove cubeMove;
	public float deltaTime;
	public FaderManager faderManager;
	
	private bool isPlayerTurn;

	void Start(){
		isPlayerTurn = false;
		cameraMove.setToInitialPoint();
		cubeMove.setToInitialPoint();
		cameraMove.toFocusPoint(deltaTime);
		IEnumerator coroutine = WaitForSliding ();
		StartCoroutine (coroutine);
	}

	void Update(){
		if(isPlayerTurn && Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, 100.0f)){
				if(hit.transform != null && hit.transform.tag == "level"){
					levelSelected(hit.transform.name);
				}
			}
		}
   	}

	private void levelSelected(string levelName){
		int last = (int)(levelName[levelName.Length - 1])-48;
		cameraMove.toFullShotPoint(deltaTime);
		cubeMove.toSelectedPoint(last, deltaTime);
		IEnumerator coroutine = WaitForSceneStart (last);
		StartCoroutine (coroutine);
	}

	IEnumerator WaitForSliding(){
		yield return new WaitForSeconds (deltaTime);
		isPlayerTurn = true;
	}

	IEnumerator WaitForSceneStart(int levelNum){
		yield return new WaitForSeconds (deltaTime);
		cameraMove.toFocusPoint(deltaTime);
		faderManager.FadeIn();
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene(levelNum+1);
	}
}
