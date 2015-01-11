using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DailyResetController : GameController {

	Player player;
	public Text resetTime;
	public GameObject explainerText;
	public GameObject startButton;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		InvokeRepeating("UpdateResetText", 0f, 1f);
		DetectAttempted();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateResetText () {
		resetTime.text = string.Format("Resets in: {0}", DailyApiController.resetTime);
	}

	void DetectAttempted () {
		if (player.attemptedDaily) {
			startButton.SetActive(false);
			explainerText.SetActive(false);
		}
	}
}
