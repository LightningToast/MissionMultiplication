using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReact : MonoBehaviour {
	public float waitTime = 0.5f;
	public Color indicateColor;
	public bool movePos = false;
	Color currCol;

	public bool moveAd = false;
	ADManager ads;
	public Vector2 altPos;
	public Vector2 originPos;
	// Use this for initialization
	void Start () {
		currCol = this.GetComponent<TextMesh> ().color;
		originPos = this.transform.localPosition;
		if(moveAd) {
			ads = GameObject.Find ("Spawn").GetComponent<ADManager> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(moveAd) {
			if(!ads.adAvailible) {
				transform.localPosition = altPos;
			} else if(movePos) {
				transform.localPosition = altPos;
			} else {
				transform.localPosition = originPos;
			}
		}
	}
	public void indicateTapped () {
		StartCoroutine (flashColor ());
	}

	IEnumerator flashColor () {
		this.GetComponent<TextMesh> ().color = indicateColor;
		yield return new WaitForSeconds (waitTime);
		this.GetComponent<TextMesh> ().color = currCol;

	}
}
