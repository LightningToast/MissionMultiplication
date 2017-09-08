using UnityEngine;
using System.Collections;

public class GunBehavior : MonoBehaviour {

	public float projectileSpeed = 20.0f;
	public GameObject projectile;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fire() {
		GameObject obj = (GameObject)Instantiate (projectile, transform.position, transform.rotation);
		obj.GetComponent<Rigidbody2D> ().velocity = new Vector2 (projectileSpeed, 0);
		Destroy (obj, 5);
	}
}
