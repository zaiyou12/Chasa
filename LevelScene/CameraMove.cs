using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public Vector3 fullShotPos;
	public Vector3 fullShotRot;
	public Vector3 focusPos;
	public Vector3 focusRot;

	public void setToInitialPoint(){
		gameObject.transform.position = fullShotPos;
		gameObject.transform.eulerAngles = fullShotRot;
	}

	public void toFocusPoint(float deltaTime){
		iTween.MoveTo (gameObject, iTween.Hash("position", focusPos, "easeType", "easeInOutSine", "time", deltaTime));
		iTween.RotateTo (gameObject, iTween.Hash("rotation", focusRot, "easeType", "easeInOutSine", "time", deltaTime));
	}

	public void toFullShotPoint(float deltaTime){
		iTween.MoveTo (gameObject, iTween.Hash("position", fullShotPos, "easeType", "easeInOutSine", "time", deltaTime));
		iTween.RotateTo (gameObject, iTween.Hash("rotation", fullShotRot, "easeType", "easeInOutSine", "time", deltaTime));
	}
}
