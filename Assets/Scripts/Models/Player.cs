using UnityEngine;
using System.Collections;

public class Player : Agent {

	[System.Serializable]
	public class Properties {
		public float speed;
		public float courage;
		public int level;
		public int kills;
		public int gold;

		public Direction direction;
	}

	public bool		Dead { get; set; }
	public int 	 	Kills { get; set; }
	public int 		Gold { get; set; }

	// Player Input
	public Agent.Direction NextDirection;

	public Player (Properties props) {
		Dead = false;
		Courage = props.courage;
		Kills = props.kills;
		Level = props.level;
		Gold = props.gold;
		CurrentDirection = props.direction;
		CurrentSpeed = props.speed;
		EnterState(Agent.State.Controlled);
	}

}
