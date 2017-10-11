using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaderManager : MonoBehaviour {

	public Image image;
	private float targetAlpha;
	public float FadeRate = 2f;

	// Use this for initialization
	void Start () {
		
		if(image == null){
			Debug.LogError("Error: No image on "+this.name);
		}
		this.targetAlpha = this.image.color.a;
		FadeOut();
	}
	
	// Update is called once per frame
	void Update () {
		Color curColor = this.image.color;
		float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
		if (alphaDiff > 0.1f){
			curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate*Time.deltaTime);
			this.image.color = curColor;
		}else{
			curColor.a = targetAlpha;
			this.image.color = curColor;
		}
	}

	public void FadeOut(){
		this.targetAlpha = 0.0f;
	}

	public void FadeIn(){
		this.targetAlpha = 1.0f;
	}
}
