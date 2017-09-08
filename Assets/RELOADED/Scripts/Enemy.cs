using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int fragments = 0;
	public GameObject[] fragment;
	public GameObject[] shards;
	public int scoreVal = 1;
	Vector2 bounds;
	GameController controller;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		bounds = Camera.main.GetComponent<GameController> ().getBounds ();
		controller = Camera.main.GetComponent<GameController> ();
		rb = this.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		if(controller.getSpeed() > 1) {
			rb.velocity = new Vector2 (2*-controller.getSpeed (), 0);
			GetComponent<PolygonCollider2D> ().enabled = false;
		}
		if((transform.position.y > -bounds.y + 2) || (transform.position.y < bounds.y - 2) || 
			(transform.position.x > bounds.x + 2) || (transform.position.x < -bounds.x - 2)){
			//GetComponent<BoxCollider2D> ().enabled = false;
			Destroy (this.gameObject);
		}
	}
	void Fragment () {
		for (int shardOuter = 0; shardOuter < 2; shardOuter++) {
			for (int y = 0; y < shards.Length; y++) {
				float rot = Random.Range (0.0f, 1.0f);
				float xSDir = Random.Range (-25f, 100.0f);
				float ySDir = Random.Range (-100.0f, 100.0f);
				GameObject sObj = (GameObject)Instantiate (shards [y], transform.position, transform.rotation);
				sObj.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (xSDir, ySDir) * 1);
				sObj.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-200.0f, 0) * 1);
				sObj.transform.Rotate (0, 0, rot * 90);
			}
		}
		for(int x = 0; x < fragment.Length; x ++) {
			for(int y = 0; y < shards.Length; y++) {
				float xSDir = Random.Range (-25f, 50.0f);
				float ySDir = Random.Range (-75.0f, 75.0f);
				GameObject sObj = (GameObject)Instantiate (shards[y], transform.position, transform.rotation);
				sObj.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (xSDir, ySDir)*6);
				sObj.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-200.0f, 0) * 1);
			}
			float xDir = Random.Range (-25f, 50.0f);
			float yDir = Random.Range (-50.0f, 50.0f);
			float xPos = Random.Range (-0.4f, 0.4f);
			float yPos = Random.Range (-0.4f, 0.4f);

			GameObject obj = (GameObject)Instantiate (fragment[x], new Vector2(transform.position.x + xPos, transform.position.y + yPos), transform.rotation);
			obj.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (xDir, yDir) * 3);
		}
		Destroy (this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Projectile"){
			Fragment ();
			GameObject.Find ("Player").GetComponent<Movement> ().score += scoreVal;
			Destroy (coll.gameObject);
		}

	}
}
