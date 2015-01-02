using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Agent {

	static string GOLD = "gold";
	static string SCORE = "points";
	static string SWORDLEVEL = "swordlevel";
	static string HEARTS = "hearts";

	[System.Serializable]
	public class Properties {
		public float speed;
		public float maxSpeed;
		public float courage;
		public int level;
		public int kills;
		public int gold;
		public float comboHoldTime;
		public int swords;
		public int swordLevel;
		public int hearts;
		public int keys;

		public Direction direction;
	}

	public float 	MaxSpeed { get; set; }
	public int		Points { get; set; }
	public int 		HighScore { get; set; }
	public bool		Dead { get; set; }
	public int 	 	Kills { get; set; }
	public int 		Gold { get; set; }
	public float 	Accel { get; set; }
	public int 		CurrentCombo { get; set; }
	public float	ComboHoldTime { get; set; }
	public int 		Swords { get; set; }
	public int 		SwordLevel { get; set; }
	public int 		Hearts { get; set; }
	public int 		MaxHearts { get; set; }
	public int 		Keys { get; set; }

	public WaypointService waypointService;
	public List<Vector3> waypointsTraversed { get; set; }
	public Vector3 lastWaypointTraversed {
		get {
			if (waypointsTraversed.Count < 1) {
				return CurrentPosition;
			}
			return waypointsTraversed[waypointsTraversed.Count -1];
		}
	}

	// Player Input

	public Player (Properties props, GameObject _go) {
		go = _go;
		Dead = false;
		Courage = props.courage;
		Kills = props.kills;
		Level = props.level;
		Gold = props.gold;
		CurrentDirection = props.direction;
		CurrentSpeed = props.speed;
		MaxSpeed = props.maxSpeed;
		CurrentCombo = 0;
		ComboHoldTime = props.comboHoldTime;
		Swords = props.swords;
		MaxHearts = props.hearts;
		Hearts = MaxHearts;
		Keys = props.keys;
		EnterState(Agent.State.Controlled);

		waypointService = new WaypointService(this);
		Load();
	}

	public void QueueNextDirection (Direction next) {
		if (CurrentDirection == Direction.Stop || isOppositeDirection(CurrentDirection, next)) {
			CurrentDirection = next;
		} 

		NextDirection = next;
	}

	public void Save () {
		SaveGold();
		SaveScore();
		SaveSwordLevel();
		SaveHearts();
	}

	public void SaveGold () {
		ES2.Save(Gold, GOLD);
	}

	public void SaveScore () {
		if (ES2.Exists (SCORE)) {
			int prevRecord = ES2.Load<int>(SCORE);
			if (Points <= prevRecord) {
				return;
			}
		} 

		ES2.Save(Points, SCORE);
	}

	public void SaveSwordLevel () {
		ES2.Save(SwordLevel, SWORDLEVEL);
	}

	public void SaveHearts () {
		ES2.Save(MaxHearts, HEARTS);
	}

	public void Load () {
		LoadGold();
		LoadScore();
		LoadSwordLevel();
		LoadHearts();
	}

	void LoadGold () {
		if (ES2.Exists(GOLD)) {
			Gold = ES2.Load<int>(GOLD);
		}
	}

	void LoadScore () {
		if (ES2.Exists(SCORE)) {
			HighScore = ES2.Load<int>(SCORE);
		}
	}

	void LoadSwordLevel () {
		if (ES2.Exists(SWORDLEVEL)) {
			SwordLevel = ES2.Load<int>(SWORDLEVEL);
		}
	}

	void LoadHearts () {
		if (ES2.Exists(HEARTS)) {
			MaxHearts = ES2.Load<int>(HEARTS);
		}
	}

	void ResetSaveData () {
		ES2.Delete(GOLD);
		ES2.Delete(SCORE);
		ES2.Delete(SWORDLEVEL);
		ES2.Delete(HEARTS);
	}

}
