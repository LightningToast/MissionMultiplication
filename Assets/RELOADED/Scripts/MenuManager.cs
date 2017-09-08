using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	TextMesh minVal;
	TextMesh maxVal;

	int min;
	int max;

	// Use this for initialization
	void Start () {
		minVal = GameObject.Find ("MinValue").GetComponent<TextMesh>();
		maxVal = GameObject.Find ("MaxValue").GetComponent<TextMesh>();
		if (PlayerPrefs.GetInt ("Min") > 0) {
			min = PlayerPrefs.GetInt ("Min");
			max = PlayerPrefs.GetInt ("Max");

		} else {
			PlayerPrefs.SetInt ("Min", 1);
			PlayerPrefs.SetInt ("Max", 9);
			PlayerPrefs.SetInt ("EarlyReloaded", 1);
			PlayerPrefs.SetFloat ("Sound", 1);
			min = 1;
			max = 9;
		}
		minVal.text = "" + min;
		maxVal.text = "" + max;
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Touch touch in Input.touches) {
			Vector2 touchPos2D = Camera.main.ScreenToWorldPoint (touch.position);
			if (touch.phase == TouchPhase.Began) {
				
				RaycastHit2D hitInformation = Physics2D.Raycast (touchPos2D, Camera.main.transform.forward);
				if (hitInformation.collider != null) {
					GameObject touchedObject = hitInformation.transform.gameObject;
					if (touchedObject.name.Equals ("Play")) {
						touchedObject.GetComponent<ButtonReact> ().indicateTapped ();
						SceneManager.LoadScene ("Game");
					} else if (touchedObject.name.Equals ("MinDown")) {
						touchedObject.GetComponent<ButtonReact> ().indicateTapped ();
						if(min > 1) {
							min--;
							minVal.text = "" + min;
							PlayerPrefs.SetInt ("Min", min);
						}
					} else if (touchedObject.name.Equals ("MinUp")) {
						touchedObject.GetComponent<ButtonReact> ().indicateTapped ();
						min++;
						minVal.text = "" + min;
						PlayerPrefs.SetInt ("Min", min);
						
					} else if (touchedObject.name.Equals ("MaxDown")) {
						touchedObject.GetComponent<ButtonReact> ().indicateTapped ();
						if(max > 1) {
							max--;
							maxVal.text = "" + max;
							PlayerPrefs.SetInt ("Max", max);
						}

					} else if (touchedObject.name.Equals ("MaxUp")) {
						touchedObject.GetComponent<ButtonReact> ().indicateTapped ();
						max++;
						maxVal.text = "" + max;
						PlayerPrefs.SetInt ("Max", max);
					}
					else if (touchedObject.name.Equals ("SoundControl")) {
						touchedObject.GetComponent<SoundControl> ().toggle ();
					}
				}
			}
		}
	}
}
