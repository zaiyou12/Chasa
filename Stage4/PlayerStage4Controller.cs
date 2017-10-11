using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class PlayerStage4Controller : MonoBehaviour {

	public float speed = 0.5f;

	Vector3 movement;
    Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        float moveHorizontal = CnInputManager.GetAxisRaw("Horizontal");
        float moveVertical = CnInputManager.GetAxisRaw ("Vertical");
		if(moveHorizontal==0.0f && moveVertical==0.0f){
			moveHorizontal = Input.GetAxisRaw ("Horizontal");
			moveVertical = Input.GetAxisRaw ("Vertical");
		}

        movement.Set(moveHorizontal, 0.0f, moveVertical);
		movement = movement.normalized * speed * Time.deltaTime;

        rb.MovePosition (transform.position + movement);
    }
}
