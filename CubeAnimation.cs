using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnimation : MonoBehaviour {

	public Vector3[] cubePos;
	public Vector3[] cubeRot;
	private bool inAnim = false;
	private int currentNum = 0;
	private floot deltaTime = 2f;

	public void Update(){
		if(!inAnim){
			inAnim = true;
			currentNum ++;
			if(currentNum >=6 ){
				currentNum = 0;
			}
			IEnumerator coroutine = AnimationOn (currentNum, deltaTime);
			StartCoroutine (coroutine);
		}
	}

	IEnumerator AnimationOn(int num, float deltaTime){
		iTween.MoveTo (gameObject, iTween.Hash("position", cubePos[num], "easeType", "easeInOutSine", "time", deltaTime));
		iTween.RotateTo (gameObject, iTween.Hash("rotation", cubeRot[num], "easeType", "easeInOutSine", "time", deltaTime));
		yield return new WaitForSeconds (deltaTime);
		iTween.RotateAdd (gameObject, iTween.Hash("rotation", cubeRot[num], "easeType", "easeInOutSine", "time", deltaTime));
	}
}
