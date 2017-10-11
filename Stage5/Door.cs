using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public DoorManager doormanager;

	public void OnTriggerEnter (Collider other){
		doormanager.OnTriggerEntered();
	}
}
