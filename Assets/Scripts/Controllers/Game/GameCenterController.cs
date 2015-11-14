using UnityEngine;
using System.Collections;

public class GameCenterController : GameController {

	public static string NORMAL_ID = "MazingRunNormalLeaderboard";
	public static string DAILY_ID = "MazingRunDailyLeaderboard";

	// Use this for initialization
	void Start () {
#if UNITY_IPHONE
		GameCenterBinding.authenticateLocalPlayer();
#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowNormalLeaderboard () {
#if UNITY_IPHONE
		GameCenterBinding.showLeaderboardWithTimeScopeAndLeaderboard(GameCenterLeaderboardTimeScope.AllTime, NORMAL_ID);
#endif
	}

	public void ShowDailyLeaderboard () {
#if UNITY_IPHONE
		GameCenterBinding.showLeaderboardWithTimeScopeAndLeaderboard(GameCenterLeaderboardTimeScope.Today, DAILY_ID);
#endif
	}

	public void ReportNormalScore (int score) {
#if UNITY_IPHONE
		GameCenterBinding.reportScore(score, NORMAL_ID);
#endif
	}

	public void ReportDailyScore (int score) {
#if UNITY_IPHONE
		GameCenterBinding.reportScore(score, DAILY_ID);
#endif
	}
}
