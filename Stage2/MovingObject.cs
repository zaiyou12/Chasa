using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

	public void MoveObject(Vector2 deltaPosVec2, float deltaTime){
		Vector3 deltaPos = gameObject.transform.position + new Vector3(deltaPosVec2.x, 0, deltaPosVec2.y);
		iTween.MoveTo (gameObject, iTween.Hash("x", deltaPos.x, "z", deltaPos.z, "easeType", "easeInOutSine", "time", deltaTime));
	}
}
