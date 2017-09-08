using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public bool controlLock = false;

	ADManager ads;
	GameObject gameOverButtons;

	public bool up;
	public bool down;
	public float speed = 2.0f;
	public GameObject gun;
	public float xPos = -6.5f;
	public int score;

	bool immune = false;

	Rigidbody2D rb;
	TouchInput input;
	Vector2 bound;

	Animator fireAnim;
	Animator laserAnim;

	// Use this for initialization
	void Start () {
		ads = GameObject.Find ("Spawn").GetComponent<ADManager> ();
		gameOverButtons = GameObject.Find ("GameOver");
		//gameOverButtons.SetActive (false);
		ads.character = this;
		rb = GetComponent<Rigidbody2D> ();
		input = Camera.main.GetComponent<TouchInput> ();
		float screenXBound = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, 0)).x;
		float screenYBound = Camera.main.ScreenToWorldPoint (new Vector2 (0, 0)).y;
		bound = new Vector2(screenXBound, screenYBound);
		fireAnim = this.transform.GetChild (0).gameObject.GetComponent<Animator> ();
		laserAnim = this.transform.GetChild (1).gameObject.GetComponent<Animator> ();
		xPos = -Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width, 0)).x + 3;
		GameObject.Find ("Score").transform.position = new Vector2 (xPos - 1f, GameObject.Find ("Score").transform.position.y);
		GameObject.Find ("Pause").transform.position = new Vector2 (-xPos + 1f, GameObject.Find ("Score").transform.position.y);
		Debug.Log (GameObject.Find ("Pause").transform.position);

	}
	
	// Update is called once per frame
	void Update () {
		
		if(transform.position.x < xPos) {
			rb.velocity = new Vector2 (1.5f, rb.velocity.y);
		} else if(transform.position.x > xPos + 0.2f) {
			rb.velocity = new Vector2 (-1.5f, rb.velocity.y);
		} else {
			rb.velocity = new Vector2 (0f, rb.velocity.y);
		}
		if ((Time.timeScale > 0) && (!controlLock)) {
			if (input.shoot) {
				//print ("SHOT");
				gun.GetComponent<GunBehavior> ().Fire ();
				this.GetComponent<AudioSource> ().Play ();
				input.resetShot ();
				laserAnim.SetTrigger ("fire");
			}
			if (input.up) {
				if (transform.position.y < -bound.y) {
					rb.velocity = new Vector2 (rb.velocity.x, 1 * speed);
					fireAnim.SetInteger ("dir", 1);
				} else {
					rb.velocity = new Vector2 (rb.velocity.x, 0);
					fireAnim.SetInteger ("dir", 0);
				}
			} else if (input.down) {
				if (transform.position.y > bound.y) {
					rb.velocity = new Vector2 (rb.velocity.x, -1 * speed);
					fireAnim.SetInteger ("dir", -1);
				} else {
					rb.velocity = new Vector2 (rb.velocity.x, 0);
					fireAnim.SetInteger ("dir", 0);
				}
			} else {
				fireAnim.SetInteger ("dir", 0);
				if (Mathf.Abs (transform.position.y) > Mathf.Abs (bound.y)) {
					rb.velocity = new Vector2 (0, 0);
				
				}
				//rb.velocity = new Vector2 (rb.velocity.x, 0);
				/*if (transform.position.y > 0.1) {
					rb.velocity = new Vector2 (rb.velocity.x, -0.5f * ((6f-transform.position.y) / 1));
				} else if (transform.position.y < -0.1) {
					rb.velocity = new Vector2 (rb.velocity.x, 0.5f * ((6f+transform.position.y) / 1));
				} else {
					rb.velocity = new Vector2 (rb.velocity.x, 0);
				}*/
			}
		}
		GameObject.Find ("Score").GetComponent<TextMesh> ().text = "Score: " + score;
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			if (!immune) {
				controlLock = true;
				gameOverButtons.SetActive (true);

				gameOver ();
				if (ads.adAvailible) {
					Time.timeScale = 0;
					continueCheck ();
				} else {
					//gameOver ();
				}
			}
		}
	}
	void continueCheck () {
		
	}

	public void continueGame () {
		controlLock = false;
		gameOverButtons.SetActive (false);
		this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		StartCoroutine (immuneTime ());
	}

	IEnumerator immuneTime () {
		immune = true;
		Color transCol = Color.white;
		transCol.a = 0.5f;
		this.GetComponent<SpriteRenderer> ().color = transCol;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		yield return new WaitForSeconds (3);
		immune = false;
		this.GetComponent<SpriteRenderer> ().color = Color.white;
		rb.constraints = RigidbodyConstraints2D.None;
	}

	public void gameOver () {
		if(PlayerPrefs.GetInt("HighScore") < score) {
			PlayerPrefs.SetInt ("HighScore", score);
		}
		GameObject.Find ("GameOver").transform.GetChild (0).GetComponent<TextMesh>().text = "High score: " + PlayerPrefs.GetInt ("HighScore");
	}

}
