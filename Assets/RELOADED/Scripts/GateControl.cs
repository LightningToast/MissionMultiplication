using UnityEngine;
using System.Collections;

public class GateControl : MonoBehaviour {
	Vector2 bound;
	Rigidbody2D rb;
	bool end, check;
	Multiplication mult;
	GameController control;
	public GameObject player;
	public AudioClip correctSound;
	public AudioClip incorrectSound;

	public Color correctCol;
	public Color incorrectCol;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		mult = Camera.main.GetComponent<Multiplication> ();
		control = Camera.main.GetComponent<GameController> ();
		float screenXBound = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, 0)).x;
		float screenYBound = Camera.main.ScreenToWorldPoint (new Vector2 (0, 0)).y;
		bound = new Vector2(screenXBound, screenYBound);
		Question ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if((!check)&&(transform.position.x < -bound.x + 3)) {
			check = true;
			print ("check");

			int decision = 0;
			if(player.transform.position.y > 1.6f) {
				decision = 0;
			} else if(player.transform.position.y < -1.6f) {
				decision = 2;
			} else {
				decision = 1;
			}
			if(mult.getVal(5) == decision) {
				this.GetComponent<AudioSource> ().PlayOneShot (correctSound);
				print ("CORRECT");
				control.setSpeed (15);
				rb.velocity = new Vector2 (-10f, 0);
				Color transparent = player.GetComponent<SpriteRenderer> ().color;
				transparent.a = 0.5f;
				player.GetComponent<SpriteRenderer> ().color = transparent;
				print (control.getSpeed ());
			} else {
				this.GetComponent<AudioSource> ().PlayOneShot (incorrectSound);
				print ("WRONG");
				control.setSpawnModifier (100);
			}
			for(int count = 0; count < 3; count ++) {
				this.transform.GetChild (count).gameObject.GetComponent<TextMesh> ().color = incorrectCol;
			}
			this.transform.GetChild (mult.getVal(5)).gameObject.GetComponent<TextMesh> ().color = correctCol;
		}
		if((!end)&&(transform.position.x < -bound.x + 0.5f)) {
			control.setSpawnModifier (1);
			//control.setSpeed (1);
	
		}
		if((!end)&&(transform.position.x < -bound.x - 8)) {
			end = true;
			rb.velocity = new Vector2 (0.0f, 0);
			control.setSpeed (1);
			control.setSpawnModifier (1);
			player.GetComponent<SpriteRenderer> ().color = Color.white;
			StartCoroutine(Delay());
		}
	}

	public void Question() {
		control.setSpawnModifier (0);
		end = false;
		check = false;
		transform.position = new Vector2 (bound.x + 0.5f, 0);
		mult.generateChoices ();
		rb.velocity = new Vector2 (-2.0f, 0);

	}

	IEnumerator Delay() {
		print(Time.time);

		yield return new WaitForSeconds(3);

		for(int count = 0; count < 3; count ++) {
			this.transform.GetChild (count).gameObject.GetComponent<TextMesh> ().color = Color.white;
		}
		Question ();
		print(Time.time);
	}
}
