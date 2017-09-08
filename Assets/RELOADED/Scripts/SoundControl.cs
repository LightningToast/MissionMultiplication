using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour {
	public Color deactivatedCol;
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetFloat ("Sound") == 0) {
			this.GetComponent<SpriteRenderer> ().color = deactivatedCol;
		} else {
			this.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void toggle () {
		if(PlayerPrefs.GetFloat ("Sound") > 0) {
			PlayerPrefs.SetFloat ("Sound", 0);
			this.GetComponent<SpriteRenderer> ().color = deactivatedCol;
		} else {
			PlayerPrefs.SetFloat ("Sound", 1);
			this.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		GameObject.Find ("MenuMusic").gameObject.GetComponent<SoundManager> ().updateSound ();
	}
}
