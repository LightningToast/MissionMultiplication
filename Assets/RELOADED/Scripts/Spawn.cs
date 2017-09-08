using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public int spawnRateScene = 1;
	public int spawnRateEnemy = 1;
	public Vector2 sceneRange = new Vector2 (50, 150);
	public Vector2 enemyRange = new Vector2 (50, 150);

	public GameObject star;
	public float starSpeed = 10;

	public GameObject[] asteroid;
	public Vector2 movementDir = new Vector2 (-10, 10);

	float sceneTrigger = 100f;
	float enemyTrigger = 100f;
	int sceneTotal = 0;
	int enemyTotal = 0;
	int asteroidCount = 0;
	float screenXBound;
	float screenYBound;

	GameController control;
	// Use this for initialization
	void Start () {
		control = Camera.main.GetComponent<GameController> ();
		screenXBound = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, 0)).x;
		screenYBound = Camera.main.ScreenToWorldPoint (new Vector2 (0, 0)).y;
		transform.position = new Vector2 (screenXBound, 0);

		//Camera.main.ScreenToWorldPoint (Screen.width, 0);
	}

	// Update is called once per frame
	void Update () {
		sceneTotal += spawnRateScene * (int) Time.timeScale;
		if (asteroid.Length > 0) {
			enemyTotal += spawnRateEnemy * control.getSpawnModifier () * (int)Time.timeScale;
		}
		if(sceneTotal >= sceneTrigger) {
			SpawnScene ();
			sceneTotal = 0;
			sceneTotal = 0;
			sceneTrigger = Random.Range (sceneRange.x, sceneRange.y);
		}
		if (enemyTotal >= enemyTrigger) {
			SpawnEnemy ();
			enemyTotal = 0;
			enemyTrigger = Random.Range (enemyRange.x, enemyRange.y);
		}
	}
	void SpawnScene () {
		//print ("SCENE");
		int scale = (int)Random.Range (1, 3);
		Vector2 spawnLoc = new Vector2 (transform.position.x, Random.Range (-screenYBound, screenYBound));
		GameObject obj = (GameObject)Instantiate (star, spawnLoc, transform.rotation);
		obj.GetComponent<StarMovement>().initialize(scale, new Vector2 (-starSpeed * 1/scale, 0));
		Destroy (obj, 8);
	}
	void SpawnEnemy () {
		//print ("ENEMY");
		//int asteroidNum = (int)Random.Range (0, asteroid.Length);
		float rot = Random.Range (0.0f, 1.0f);
		Vector2 spawnLoc = new Vector2 (transform.position.x + 0.5f, Random.Range (-screenYBound, screenYBound));
		GameObject obj = (GameObject)Instantiate (asteroid[asteroidCount], spawnLoc, transform.rotation);
		obj.transform.Rotate (0, 0, rot * 90);
		float mod = 1.0f;
		if(asteroidCount > 5) {
			mod = 0.5f;
		}
		float x = Random.Range (movementDir.x, -50.0f);
		float y = Random.Range (-movementDir.y, movementDir.y);
		obj.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (mod*x, y)*6);
		asteroidCount = Random.Range (0, asteroid.Length);

	}
}
