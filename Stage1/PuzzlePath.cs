using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePath : MonoBehaviour {

	public Transform whiteBasePosition;
	public Transform blackBasePosition;
	public Transform playerPos;
	public float BlockSize;

	private bool isWhite;
	private float playerYPos;
	private Vector2 deltaPos;
	// 0 - floor, 1 - ice
	private static int xMax = 6;
	private static int yMax = 6;
	private int[,] whitePuzzlePath = new int [6, 6] {
		{ 1, 0, 1, 1, 1, 1 },
		{ 0, 1, 1, 1, 0, 0 },
		{ 1, 1, 0, 0, 1, 1 },
		{ 1, 1, 1, 1, 1, 1 },
		{ 1, 0, 1, 1, 1, 1 },
		{ 0, 1, 1, 1, 1, 0 }
	};
	private int[,] blackPuzzlePath = new int [6, 6]{
		{0, 0, 1, 1, 1, 1 },
		{1, 1, 1, 0, 0, 1 },
		{1, 1, 0, 1, 1, 0 },
		{0, 1, 1, 0, 1, 1 },
		{1, 0, 1, 1, 0, 0 },
		{1, 1, 1, 1, 0, 1 }
	};

	private void Start(){
		isWhite = true;
		playerYPos = playerPos.position.y;
		playerPos.position = new Vector3(whiteBasePosition.position.x, playerYPos, whiteBasePosition.position.z);
	}

	// Move Player
	public bool CanMove(int horizontal, int vertical, Vector3 playerPos){
		Vector2 currentPos = ToPuzzlePoint (playerPos);

		if ((horizontal > 0 && currentPos.y >= yMax) || (horizontal < 0 && currentPos.y <= 0)) {
			return false;
		} else if ((vertical > 0 && currentPos.x <= 0) || (vertical < 0 && currentPos.x >= xMax)) {
			return false;	
		}

		deltaPos = new Vector2 (0, 0);
		int[,] puzzlePath;
		if (isWhite){
			puzzlePath = whitePuzzlePath;
		}else{
			puzzlePath = blackPuzzlePath;
		}

		if (horizontal > 0) {
			for (int i = (int)currentPos.y + 1; i < yMax; i++) {
				if (puzzlePath [(int)currentPos.x, i] == 0) {
					deltaPos.y = i - currentPos.y;
					return true;
				}
			}
		} else if (horizontal < 0) {
			for (int i = (int)currentPos.y - 1; i >= 0; i--) {
				if (puzzlePath [(int)currentPos.x, i] == 0) {
					deltaPos.y = i - currentPos.y;
					return true;
				}
			}
		} else if (vertical > 0) {
			for (int i = (int)currentPos.x - 1; i >= 0; i--) {
				if (puzzlePath [i, (int)currentPos.y] == 0) {
					deltaPos.x = i - currentPos.x;
					return true;
				}
			}
		} else {
			for (int i = (int)currentPos.x + 1; i < xMax; i++) {
				if (puzzlePath [i, (int)currentPos.y] == 0) {
					deltaPos.x = i - currentPos.x;
					return true;
				}
			}
		}

		return false;
	}

	public Vector3 GetDeltaPos(){
		return ToWorldPoint(deltaPos);
	}
	/*
	public float GetDeltaTime(){
		return Mathf.Sqrt (Mathf.Abs (deltaPos.x) + Mathf.Abs (deltaPos.y));
	}*/

	private Vector2 ToPuzzlePoint(Vector3 playerPos){
		Vector3 basePos;
		if (isWhite){
			basePos = whiteBasePosition.position;
		} else{
			basePos = blackBasePosition.position;
		}
		Vector3 deltaPosVec3 = (playerPos - basePos)/BlockSize;
		Vector2 result = new Vector2 (-Mathf.Round(deltaPosVec3.z) + 5, Mathf.Round(deltaPosVec3.x));
		return result;
	}

	private Vector3 ToWorldPoint(Vector2 puzzlePos){
		return new Vector3 (puzzlePos.y * BlockSize, 0, -puzzlePos.x * BlockSize);
	}

	// Switch Map
	public bool CanSwitchMap(Vector3 playerPos){
		int[,] puzzlePath;
		if (isWhite){
			puzzlePath = blackPuzzlePath;
		} else {
			puzzlePath = whitePuzzlePath;
		}
		
		Vector2 currentPos = ToPuzzlePoint (playerPos);
		if (puzzlePath[(int)currentPos.x, (int)currentPos.y] == 0){
			return true;
		}else{
			return false;
		}
	}

	public void SwitchMap(float yPos){
		Vector3 deltaPosVec3 = blackBasePosition.position - whiteBasePosition.position;
		if (isWhite) {
			playerPos.position += deltaPosVec3;
			isWhite = false;
		} else {
			playerPos.position -= deltaPosVec3;
			isWhite = true;
		}
	}
}
