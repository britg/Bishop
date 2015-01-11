using UnityEngine;
using System.Collections;

public class DailyApiController : GameController {

	public static string seedEndpoint = "http://foolishaggro.com/mazing/daily";
	public static string DAILY_SEED = "daily_seed";

	// Use this for initialization
	void Start () {
		LocalDailySeed();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LocalDailySeed () {
		int seed = (int)System.DateTime.UtcNow.Date.Ticks;
		Debug.Log ("Seed is " + seed);
		SaveSeed(seed);
	}

	IEnumerator GetDailySeed () {
		WWW www = new WWW(seedEndpoint);
		yield return www;
		string seedText = www.text;
		Debug.Log ("Daily seed is " + seedText);
		SaveSeed(int.Parse(seedText));
	}

	void SaveSeed (int seed) {
		ES2.Save(seed, DAILY_SEED);
	}

}
