using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class PlayerStage1Controller : MonoBehaviour {

	public PuzzlePath puzzlePath;
	public Transform playerPos;
	public float speedToTime = 1.5f;

	private bool playersTurn = true;
	
	// Update is called once per frame
	void Update () {
		if (playersTurn) {
			InputMovement ();
		}

		bool down = Input.GetButtonDown("Jump") || CnInputManager.GetButtonDown("Jump");;
		if (playersTurn && down){
			 OnButtonClicked();
		 }
	}

	void InputMovement(){
		int horizontal = 0;
		int vertical = 0;

		horizontal = (int) (CnInputManager.GetAxisRaw("Horizontal"));
		vertical = (int) (CnInputManager.GetAxisRaw ("Vertical"));
		if(horizontal==0 && vertical==0){
			horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
			vertical = (int) (Input.GetAxisRaw ("Vertical"));
		}

		if(horizontal != 0){
			vertical = 0;
		}

		if(horizontal != 0 || vertical != 0)
		{
			playersTurn = false;
			AttempMove(horizontal, vertical);
		}
	}

	private void OnButtonClicked(){
		playersTurn = false;
		if(puzzlePath.CanSwitchMap(playerPos.position)){
			IEnumerator coroutine = WaitForSwitch (0.5f);
			StartCoroutine (coroutine);
		}else{
			playersTurn = true;
		}
	}

	private void AttempMove(int horizontal, int vertical){
		if (puzzlePath.CanMove (horizontal, vertical, playerPos.position)) {
			MoveCharacter (puzzlePath.GetDeltaPos (), speedToTime);
		} else {
			playersTurn = true;
		}
	}

	private void MoveCharacter(Vector3 deltaPos, float deltaTime){
		deltaPos = gameObject.transform.position + deltaPos;
		iTween.MoveTo (gameObject, iTween.Hash("x", deltaPos.x, "z", deltaPos.z, "easeType", "easeInOutSine", "time", deltaTime));
		IEnumerator coroutine = WaitForSliding (deltaTime);
		StartCoroutine (coroutine);
	}

	IEnumerator WaitForSliding(float seconds){
		yield return new WaitForSeconds (seconds);
		playersTurn = true;
	}

	IEnumerator WaitForSwitch(float seconds){
		float originY = gameObject.transform.position.y;
		iTween.MoveTo (gameObject, iTween.Hash("y", originY-1.0f, "easeType", "easeInOutSine", "time", seconds));
		yield return new WaitForSeconds(seconds);
		puzzlePath.SwitchMap (gameObject.transform.position.y);
		iTween.MoveTo (gameObject, iTween.Hash("y", originY, "easeType", "easeInOutSine", "time", seconds));
		yield return new WaitForSeconds(seconds);
		playersTurn = true;
	}
}
