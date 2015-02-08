using UnityEngine;
using System.Collections;

public class GameCenterController : GameController {

	public static string NORMAL_ID = "MazingRunNormalLeaderboard";
	public static string DAILY_ID = "MazingRunDailyLeaderboard";

	// Use this for initialization
	void Start () {
		GameCenterBinding.authenticateLocalPlayer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowNormalLeaderboard () {
		GameCenterBinding.showLeaderboardWithTimeScopeAndLeaderboard(GameCenterLeaderboardTimeScope.AllTime, NORMAL_ID);
	}

	public void ShowDailyLeaderboard () {
		GameCenterBinding.showLeaderboardWithTimeScopeAndLeaderboard(GameCenterLeaderboardTimeScope.Today, DAILY_ID);
	}

	public void ReportNormalScore (int score) {
		GameCenterBinding.reportScore(score, NORMAL_ID);
	}

	public void ReportDailyScore (int score) {
		GameCenterBinding.reportScore(score, DAILY_ID);
	}
}
