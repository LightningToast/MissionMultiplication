using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public float modifier = 0;
	public int spawnModifier = 1;
	float screenXBound;
	float screenYBound;
	// Use this for initialization
	void Start () {
		screenXBound = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, 0)).x;
		screenYBound = Camera.main.ScreenToWorldPoint (new Vector2 (0, 0)).y;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setSpeed(float modifier) {
		this.modifier = modifier;
	}
	public float getSpeed() {
		return modifier;
	}
	public void setBounds(float x, float y) {
		screenXBound = x;
		screenYBound = y;
	}
	public Vector2 getBounds() {
		return new Vector2 (screenXBound, screenYBound);
	}

	public void setSpawnModifier(int input) {
		spawnModifier = input;
	}
	public int getSpawnModifier() {
		return spawnModifier;
	}
}
