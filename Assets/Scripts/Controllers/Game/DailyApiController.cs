using UnityEngine;
using System.Collections;

public class DailyApiController : GameController {

	public static string seedEndpoint = "http://foolishaggro.com/mazing/daily";
	public static string DAILY_SEED = "daily_seed";

	public static long resetTicks {
		get {
			return (System.DateTime.UtcNow.AddDays(1).Date.Ticks - System.DateTime.UtcNow.Ticks);
		}
	}

	public static string resetTime {
		get {
			System.TimeSpan elapsedSpan = new System.TimeSpan(resetTicks);
			return string.Format("{0} hours, {1} minutes", 
			        elapsedSpan.Hours, 
			        elapsedSpan.Minutes);
		}
	}
	
	// Use this for initialization
	void Start () {
		LocalDailySeed();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LocalDailySeed () {
		int seed = (int)System.DateTime.UtcNow.Date.Ticks;
		Debug.Log ("Local daily seed is " + seed);
		SaveSeed(seed);
	}

	IEnumerator GetDailySeed () {
		WWW www = new WWW(seedEndpoint);
		yield return www;
		string seedText = www.text;
		Debug.Log ("Server daily seed is " + seedText);
		SaveSeed(int.Parse(seedText));
	}

	void SaveSeed (int seed) {
		ES2.Save(seed, DAILY_SEED);
	}

}
