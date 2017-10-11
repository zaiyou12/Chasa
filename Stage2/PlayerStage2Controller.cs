using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class PlayerStage2Controller : MonoBehaviour {

	public PushPuzzlePath pushPuzzlePath;
	public Transform playerPos;
	public float speedToTime = 1.5f;

	private bool playersTurn = true;

	void Update () {
		if (playersTurn) {
			InputMovement ();
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

	private void AttempMove(int horizontal, int vertical){
		if(pushPuzzlePath.PlayerCanMove(horizontal, vertical, playerPos.transform.position)){
			
		} else {
			playersTurn = true;
		}
	}

	public void MoveCharacter(Vector2 deltaPosVec2, float deltaTime){
		Vector3 deltaPos = gameObject.transform.position + new Vector3(deltaPosVec2.x, 0, deltaPosVec2.y);
		iTween.MoveTo (gameObject, iTween.Hash("x", deltaPos.x, "z", deltaPos.z, "easeType", "easeInOutSine", "time", deltaTime));
		IEnumerator coroutine = WaitForSliding (deltaTime);
		StartCoroutine (coroutine);
	}

	IEnumerator WaitForSliding(float seconds){
		yield return new WaitForSeconds (seconds);
		playersTurn = true;
	}
}
