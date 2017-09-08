using UnityEngine;
using System.Collections;

public class StarMovement : MonoBehaviour {
	Rigidbody2D rb;
	Vector2 speed;
	GameController controller;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		controller = Camera.main.GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = speed * controller.getSpeed();
	}

	public void initialize(int scale, Vector2 speed) {
		rb = GetComponent<Rigidbody2D> ();
		transform.localScale = transform.localScale * scale;
		rb.velocity = speed;
		this.speed = speed;
	}

}
