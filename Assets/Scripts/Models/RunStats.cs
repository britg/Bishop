using UnityEngine;
using System.Collections;

public class RunStats : GameModel {

	static string GOLD = "lastrungold";
	static string KILLS = "lastrunkills";
	static string DISTANCE = "lastrundistance";
	static string KILLEDBY = "lastrunkilledby";
	static string CHESTS = "lastrunchests";

	public int gold;
	public int kills;
	public float distance;
	public string killedBy;
	public int chests;

	public void AddGold (int amount) {
		gold += amount;
	}

	public void AddKills (int amount) {
		kills += amount;
	}

	public void AddDistance (float amount) {
		distance += amount;
	}

	public void AddChest (int amount) {
		chests += amount;
	}

	// Called by player.Save();
	public void Save () {
		// Save last run
		ES2.Save (gold, GOLD);
		ES2.Save (kills, KILLS);
		ES2.Save (distance, DISTANCE);
		ES2.Save (killedBy, KILLEDBY);
		ES2.Save (chests, CHESTS);
	}

	public static RunStats LastRun () {
		RunStats lastRun = new RunStats();
		lastRun.Load();
		return lastRun;
	}

	public void Load () {
		LoadGold();
		LoadKills();
		LoadDistance();
		LoadKilledBy();
		LoadChests();
	}

	void LoadGold () {
		if (ES2.Exists(GOLD)) {
			gold = ES2.Load<int>(GOLD);
		}
	}

	void LoadKills () {
		if (ES2.Exists(KILLS)) {
			kills = ES2.Load<int>(KILLS);
		}
	}

	void LoadDistance () {
		if (ES2.Exists(DISTANCE)) {
			distance = ES2.Load<float>(DISTANCE);
		}
	}

	void LoadKilledBy () {
		if (ES2.Exists(KILLEDBY)) {
			killedBy = ES2.Load<string>(KILLEDBY);
		}
	}

	void LoadChests () {
		if (ES2.Exists(CHESTS)) {
			chests = ES2.Load<int>(CHESTS);
		}
	}

}
