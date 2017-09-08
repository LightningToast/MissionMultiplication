using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ADManager : MonoBehaviour {
	public bool adAvailible = true;
	public Movement character;
	// Use this for initialization
	void Start () {
		Advertisement.Initialize ("1525656");
		adAvailible = Advertisement.isSupported;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ShowRewardedVideo ()
	{
		Time.timeScale = 0;
		var options = new ShowOptions();
		options.resultCallback = HandleShowResult;

		Advertisement.Show("rewardedVideo", options);
	}

	void HandleShowResult (ShowResult result)
	{
		adAvailible = false;
		Time.timeScale = 1;
		if(result == ShowResult.Finished) {
			Debug.Log("Video completed - Offer a reward to the player");
			character.continueGame ();
		} else if(result == ShowResult.Skipped) {
			Debug.LogWarning("Video was skipped - Do NOT reward the player");
			character.gameOver ();

		} else if(result == ShowResult.Failed) {
			Debug.LogError("Video failed to show");
			character.gameOver ();
		}
	}
}
