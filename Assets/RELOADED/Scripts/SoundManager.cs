using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	float startingVol;
	// Use this for initialization
	void Start () {
		startingVol = this.GetComponent<AudioSource> ().volume;
		this.GetComponent<AudioSource> ().volume = startingVol * PlayerPrefs.GetFloat ("Sound");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void updateSound() {
		this.GetComponent<AudioSource> ().volume = startingVol * PlayerPrefs.GetFloat ("Sound");
	}
}
