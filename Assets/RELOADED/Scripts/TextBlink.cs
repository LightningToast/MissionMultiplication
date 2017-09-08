using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBlink : MonoBehaviour {
	TextMesh text;
	public float blinkTime = 0.5f;
	// Use this for initialization
	void Start () {
		text = this.GetComponent<TextMesh> ();
		StartCoroutine (blink ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator blink () {
		text.color = Color.white;
		yield return new WaitForSeconds (blinkTime);
		text.color = Color.clear;
		yield return new WaitForSeconds (blinkTime/1.5f);
		text.color = Color.white;
		StartCoroutine (blink ());
	}
}
