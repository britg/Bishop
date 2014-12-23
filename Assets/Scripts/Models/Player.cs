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
	public float 	Accel { get; set; }

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
		EnterState(Agent.State.Controlled);
	}

	public void QueueNextDirection (Direction next) {
		if (CurrentDirection == Direction.Stop || isOppositeDirection(CurrentDirection, next)) {
			CurrentDirection = next;
		} 

		NextDirection = next;
	}

}
