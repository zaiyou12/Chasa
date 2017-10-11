using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PushPuzzlePath : MonoBehaviour {

	public Vector3 basePos;
	public MovingObject[] boxObjs;
	public PlayerStage2Controller player;
	public float BlockSize;
	public float deltaTime = 1.5f;
	public FaderManager faderManager;

	private Vector2[] objectPos = new Vector2[]{new Vector2(2,3), 
												new Vector2(2,4), 
												new Vector2(2,5)};
	private static int xMax = 3;
	private static int yMax = 5;
	// 0-floor, 1-block, 2-goal
	private int[,] pushPuzzlePath = new int[4,6]{
		{0, 0, 0, 0, 0, 1},
		{0, 2, 2, 0, 0, 0},
		{0, 1, 0, 0, 0, 0},
		{0, 0, 0, 1, 0, 2}
	};

	// Move Player
	public bool PlayerCanMove(int horizontal, int vertical, Vector3 playerPos){
		Vector2 currentPos = ToPuzzlePoint (playerPos);

		// check outer wall
		if ((horizontal > 0 && currentPos.y >= yMax) || (horizontal < 0 && currentPos.y <= 0)) {
			return false;
		} else if ((vertical > 0 && currentPos.x <= 0) || (vertical < 0 && currentPos.x >= xMax)) {
			return false;	
		}

		Vector2 dirPos = currentPos + new Vector2(-vertical, horizontal);
		int pathType = pushPuzzlePath[(int)dirPos.x, (int)dirPos.y];
		// check block
		if(pathType == 1){
			return false;
		}
		// check box
		int num = 0;
		foreach (Vector2 itemPos in objectPos)
		{
			if(itemPos==dirPos){
				if(BoxCanMove(horizontal, vertical, itemPos)){
					// move box & player
					boxObjs[num].MoveObject(ToWorldPoint(horizontal, vertical), deltaTime);
					player.MoveCharacter(ToWorldPoint(horizontal, vertical), deltaTime);
					updateBoxPos(horizontal, vertical, num);
					return true;
				}else{
					return false;
				}
			}
			num ++;
		}
		player.MoveCharacter(ToWorldPoint(horizontal, vertical), deltaTime);
		return true;
	}

	private bool BoxCanMove(int horizontal, int vertical, Vector2 itemPos){
		// check outer wall
		if ((horizontal > 0 && itemPos.y >= yMax) || (horizontal < 0 && itemPos.y <= 0)) {
			return false;
		} else if ((vertical > 0 && itemPos.x <= 0) || (vertical < 0 && itemPos.x >= xMax)) {
			return false;	
		}
		// check block
		Vector2 dirPos = itemPos + new Vector2(-vertical, horizontal);
		int pathType = pushPuzzlePath[(int)dirPos.x, (int)dirPos.y];
		if(pathType == 1){
			return false;
		}
		// check box
		int num = 0;
		foreach (Vector2 boxPos in objectPos)
		{
			if(boxPos==dirPos){
				return false;
			}
			num ++;
		}
		return true;
	}

	private Vector2 ToPuzzlePoint(Vector3 objPos){
		Vector3 deltaPosVec3 = (objPos - basePos)/BlockSize;
		Vector2 result = new Vector2 (Mathf.Round(deltaPosVec3.x), Mathf.Round(deltaPosVec3.z));
		return result;
	}

	private Vector2 ToWorldPoint(int horizontal,int vertical){
		return new Vector2(-vertical*BlockSize, horizontal*BlockSize);
	}

	private void updateBoxPos(int horizontal, int vertical, int num){
		objectPos[num] += new Vector2 (-vertical, horizontal);
		// check is cleared
		if (pushPuzzlePath[(int)objectPos[num].x, (int)objectPos[num].y] == 2){
			for(int i =0; i < 3; i++){
				if(pushPuzzlePath[(int)objectPos[i].x, (int)objectPos[i].y] != 2){
					return;
				}
			}
			faderManager.FadeIn();
			SceneManager.LoadScene(0);
		}
	}

	public void RestartScene(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
