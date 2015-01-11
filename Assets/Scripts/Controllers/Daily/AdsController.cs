using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdsController : GameController {

	Player player;
	public DailyResetController dailyResetController;

	// Use this for initialization
	void Start () {
		Advertisement.Initialize ("22556");
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowAd () {
		if (Advertisement.isReady()) { 
			Advertisement.Show(); 
		}

		player.ReverseLastDailySeed();
		dailyResetController.DetectAttempted();
	}
}
