using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TouchInput : MonoBehaviour {
	public bool up;
	public bool down;
	public bool shoot;

	bool firstTouch = true;

	ADManager ads;
	GameObject gameOverButtons;
	
	// Use this for initialization

	void Start () {
		ads = GameObject.Find ("Spawn").GetComponent<ADManager> ();
		gameOverButtons = GameObject.Find ("GameOver");
		gameOverButtons.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		bool inUp = false;
		bool inDown = false;
		foreach(Touch touch in Input.touches) {
			Vector2 touchPos2D = Camera.main.ScreenToWorldPoint (touch.position);
			if(touch.phase == TouchPhase.Began) {
				if(touchPos2D.x > 0) {
					shoot = true;
					//print ("SHOOT");
					
				}
				if(firstTouch) {
					GameObject.Find ("Instructions").SetActive (false);
					firstTouch = false;
				}
				RaycastHit2D hitInformation = Physics2D.Raycast (touchPos2D, Camera.main.transform.forward);
				if (hitInformation.collider != null) {
					GameObject touchedObject = hitInformation.transform.gameObject;
					if (touchedObject.name.Equals ("Continue")) {
						touchedObject.GetComponent<ButtonReact> ().indicateTapped ();
						ads.ShowRewardedVideo ();
					} else if (touchedObject.name.Equals ("Restart")) {
						touchedObject.GetComponent<ButtonReact> ().indicateTapped ();
						Time.timeScale = 1;
						SceneManager.LoadScene ("Game");
					} else if (touchedObject.name.Equals ("Menu")) {
						touchedObject.GetComponent<ButtonReact> ().indicateTapped ();
						Time.timeScale = 1;
						SceneManager.LoadScene ("MainMenu");
					} else if (touchedObject.name.Equals ("Pause")) {
						touchedObject.GetComponent<ButtonReact> ().indicateTapped ();
						if (Time.timeScale < 1) {
							Time.timeScale = 1;
							gameOverButtons.GetComponent<TextMesh>().text = "Game Over!";
							gameOverButtons.transform.GetChild (0).gameObject.SetActive (true);
							gameOverButtons.transform.GetChild (1).gameObject.GetComponent<ButtonReact>().movePos = false;
							gameOverButtons.transform.GetChild (2).gameObject.GetComponent<ButtonReact>().movePos = false;
							gameOverButtons.transform.GetChild (3).gameObject.SetActive (true);
							gameOverButtons.SetActive (false);
						} else {
							Time.timeScale = 0;
							gameOverButtons.SetActive (true);
							gameOverButtons.GetComponent<TextMesh>().text = "Paused";
							gameOverButtons.transform.GetChild (0).gameObject.SetActive (false);
							gameOverButtons.transform.GetChild (1).gameObject.GetComponent<ButtonReact>().movePos = true;
							gameOverButtons.transform.GetChild (2).gameObject.GetComponent<ButtonReact>().movePos = true;
							gameOverButtons.transform.GetChild (3).gameObject.SetActive (false);
						}

					}

				}
			}
			if(touchPos2D.x < 0) {
				if(touchPos2D.y > 0) {
					inUp = true;
					up = true;
				} else {
					inDown = true;
					down = true;
				}
			}

		}
		if(!inUp) {
			up = false;
		}
		if(!inDown) {
			down = false;
		}
		//shoot = false;
		//up = false;
		//down = false;
	}
	public void resetShot() {
		shoot = false;
	}
}
