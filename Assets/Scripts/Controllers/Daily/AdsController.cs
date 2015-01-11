using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdsController : GameController {


	// Use this for initialization
	void Start () {
		Advertisement.Initialize ("22556");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ShowAd () {
		if (Advertisement.isReady()) { 
			Advertisement.Show(); 
		}
	}
}
