using UnityEngine;
using System.Collections;

public class Player : Agent {

	[System.Serializable]
	public class Properties {
		public float speed;
		public float maxSpeed;
		public float courage;
		public int level;
		public int kills;
		public int gold;
		public int swords;
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
	public int 		Swords { get; set; }
	public int 		Hearts { get; set; }
	public int 		Keys { get; set; }

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
		Swords = props.swords;
		Hearts = props.hearts;
		Keys = props.keys;
		EnterState(Agent.State.Controlled);

		Load();
	}

	public void QueueNextDirection (Direction next) {
		if (CurrentDirection == Direction.Stop || isOppositeDirection(CurrentDirection, next)) {
			CurrentDirection = next;
		} 

		NextDirection = next;
	}

	public void Save () {
		ES2.Save(Gold, "gold");

		if (ES2.Exists ("points")) {
			CheckNewRecord();
		} else {
			ES2.Save(Points, "points");
		}
	}

	public void Load () {
		if (ES2.Exists("gold")) {
			Gold = ES2.Load<int>("gold");
		}

		if (ES2.Exists("points")) {
			HighScore = ES2.Load<int>("points");
		}
	}

	void CheckNewRecord () {
		int prevRecord = ES2.Load<int>("points");
		if (Points > prevRecord) {
			ES2.Save(Points, "points");
		}
	}

}
