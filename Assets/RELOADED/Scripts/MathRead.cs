using UnityEngine;
using System.Collections;

public class MathRead : MonoBehaviour {
	public int type = 1;
	Multiplication mult;
	TextMesh txt;
	// Use this for initialization
	void Start () {
		mult = Camera.main.GetComponent<Multiplication> ();
		txt = this.GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(type == 3) {
			txt.text = "" + mult.getVal (3) + " x " + mult.getVal (4);
		} else {
			txt.text = "" + mult.getVal (type);
		}
	}
}
