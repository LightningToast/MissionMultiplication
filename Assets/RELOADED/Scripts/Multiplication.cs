using UnityEngine;
using System.Collections;

public class Multiplication : MonoBehaviour {
	public int answer;
	public int x;
	public int y;
	public int[] options = new int[6];
	public int choice1;
	public int choice2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("space")) {
			generateChoices ();
		}
	}
	public void generateChoices () {
		
		x = (int) Random.Range(PlayerPrefs.GetInt ("Min"),PlayerPrefs.GetInt ("Max"));
		y = (int) Random.Range(PlayerPrefs.GetInt ("Min"),PlayerPrefs.GetInt ("Max"));

		options [3] = x;
		options [4] = y;
		int wrongOne, wrongTwo;
		while(true) {
			wrongOne = chooseOption (x, y);
			wrongTwo = chooseOption (x, y);
			if((wrongOne != wrongTwo) && (wrongOne != x * y) && 
				(wrongTwo != x * y) && (wrongOne > 0) && (wrongTwo > 0)) {
				break;
			}
		}
		int layout = (int)Random.Range (0, 3);
		switch (layout)
		{
		case 0:
			options [0] = x * y;
			options [1] = wrongOne;
			options [2] = wrongTwo;
			options [5] = 0;
			break;

		case 1:
			options [0] = wrongOne;
			options [1] = x * y;
			options [2] = wrongTwo;
			options [5] = 1;
			break;

		case 2:
			options [0] = wrongOne;
			options [1] = wrongTwo;
			options [2] = x * y;
			options [5] = 2;
			break;
		default:
			options [0] = wrongOne;
			options [1] = wrongTwo;
			options [2] = x * y;
			options [5] = 2;
			break;
		}

		print (options [0] + " " + options [1] + " " + options [2]);
	}

	int chooseOption(int x, int y) {
		int choice = (int) Random.Range (0, 19);
		switch (choice)
		{
		case 0:
			return DoubleAnswer (x, y);
		case 1:
			return HalfAnswer (x, y);
		case 2:
			return OneOverXAnswer (x, y);
		case 3:
			return OneUnderXAnswer (x, y);
		case 4:
			return TwoOverXAnswer (x, y);
		case 5:
			return TwoUnderXAnswer (x, y);
		case 6:
			return OneOverYAnswer (x, y);
		case 7:
			return OneUnderYAnswer (x, y);
		case 8:
			return TwoOverYAnswer (x, y);
		case 9:
			return TwoUnderYAnswer (x, y);
		case 10:
			return OneOverAnswer (x, y);
		case 11:
			return OneUnderAnswer (x, y);
		case 12:
			return TwoOverAnswer (x, y);
		case 13:
			return TwoUnderAnswer (x, y);
		case 14:
			return FiveOverAnswer (x, y);
		case 15:
			return FiveUnderAnswer (x, y);
		case 16:
			return TenOverAnswer (x, y);
		case 17:
			return TenUnderAnswer (x, y);
		case 18:
			return RandomAnswer (x, y);
		default:
			return RandomAnswer (x, y);
		}
	}







	public int DoubleAnswer(int x, int y) {
		return 2 * (x * y);
	}
	public int HalfAnswer(int x, int y) {
		return (int)(x * y) / 2;
	}

	public int OneOverXAnswer(int x, int y) {
		return ((x + 1) * y);
	}
	public int OneUnderXAnswer(int x, int y) {
		return ((x - 1) * y);
	}
	public int TwoOverXAnswer(int x, int y) {
		return ((x + 2) * y);
	}
	public int TwoUnderXAnswer(int x, int y) {
		return ((x - 2) * y);
	}
	public int OneOverYAnswer(int x, int y) {
		return (x * (y + 1));
	}
	public int OneUnderYAnswer(int x, int y) {
		return (x * (y - 1));
	}
	public int TwoOverYAnswer(int x, int y) {
		return (x * (y + 2));
	}
	public int TwoUnderYAnswer(int x, int y) {
		return (x * (y - 2));
	}


	public int OneOverAnswer(int x, int y) {
		return (x * y) + 1;
	}
	public int OneUnderAnswer(int x, int y) {
		return (x * y) - 1;
	}
	public int TwoOverAnswer(int x, int y) {
		return (x * y) + 2;
	}
	public int TwoUnderAnswer(int x, int y) {
		return (x * y) - 2;
	}
	public int FiveOverAnswer(int x, int y) {
		return (x * y) + 5;
	}
	public int FiveUnderAnswer(int x, int y) {
		return (x * y) - 5;
	}
	public int TenOverAnswer(int x, int y) {
		return (x * y) + 10;
	}
	public int TenUnderAnswer(int x, int y) {
		return (x * y) - 10;
	}

	public int RandomAnswer(int x, int y) {
		return (int)Random.Range (x, y);
	}

	public int getVal(int pos) {
		return options [pos];
	}
}
