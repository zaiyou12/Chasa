using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour {

	public Vector3[] cubePos;
	public Vector3[] cubeRot;

	public void setToInitialPoint(){
		gameObject.transform.position = cubePos[0];
		gameObject.transform.eulerAngles = cubeRot[0];
	}

	public void toSelectedPoint(int num, float deltaTime){
		iTween.MoveTo (gameObject, iTween.Hash("position", cubePos[num], "easeType", "easeInOutSine", "time", deltaTime));
		iTween.RotateTo (gameObject, iTween.Hash("rotation", cubeRot[num], "easeType", "easeInOutSine", "time", deltaTime));
	}
}
