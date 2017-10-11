using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour {

	public GameObject[] doors;
	public FaderManager faderManager;
	private int currentNum;

	private void Start(){
		foreach(GameObject item in doors){
			item.SetActive(false);
		}
		doors[0].SetActive(true);
		currentNum = 0;
	}

	public void OnTriggerEntered (){
		doors[currentNum].SetActive(false);
		currentNum ++;
		if(currentNum < doors.Length){
			doors[currentNum].SetActive(true);
		}else{
			faderManager.FadeIn();
			SceneManager.LoadScene(0);
		}
	}
}
