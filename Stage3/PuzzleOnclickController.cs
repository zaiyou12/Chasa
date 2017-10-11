using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleOnclickController : MonoBehaviour {

	public GameObject[] puzzleObjects;
	public FaderManager faderManager;

	void Update(){
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, 100.0f)){
				if(hit.transform != null){
					hit.transform.Rotate(new Vector3 (0f, 0f, 90f));
					if(hit.transform.eulerAngles.y < 1 || hit.transform.eulerAngles.y > -1){
						if(checkCleared()){
							faderManager.FadeIn();
							SceneManager.LoadScene(0);
						}
					}
				}
			}
		}
   	}

	private bool checkCleared(){
		foreach (GameObject item in puzzleObjects)
		{
			if(item.transform.eulerAngles.y > 1 || item.transform.eulerAngles.y < -1){
				return false;
			}
		}
		return true;
	}
}
