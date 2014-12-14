using UnityEngine;
using System.Collections;

public class Player : GameModel {

	[System.Serializable]
	public class Properties {
		public float speed;
		public float courage;
		public int level;
		public int kills;
		public int gold;

		public Direction direction;
	}

	public enum Direction {
		Up,
		Down,
		Right,
		Left,
		Stop
	}

	public bool		Dead { get; set; }
	public float 	Speed { get; set; }
	public float 	Courage { get; set; }
	public int 	 	Level { get; set; }
	public int 	 	Kills { get; set; }
	public int 		Gold { get; set; }

	public Direction CurrentDirection { get; set; }
	public Vector3 Velocity { get { return Speed * DirectionVector; } }
	public Vector3 DirectionVector {
		get {
			if (CurrentDirection == Player.Direction.Up) {
				return Vector3.forward;
			}
			if (CurrentDirection == Player.Direction.Right) {
				return Vector3.right;
			}
			if (CurrentDirection == Player.Direction.Left) {
				return Vector3.left;
			}
			if (CurrentDirection == Player.Direction.Down) {
				return Vector3.back;
			}
			if (CurrentDirection == Player.Direction.Stop) {
				return Vector3.zero;
			}

			return Vector3.zero;
		}
	}


	public Player (Properties props) {
		Dead = false;
		Speed = props.speed;
		Courage = props.courage;
		Kills = props.kills;
		Level = props.level;
		Gold = props.gold;
		CurrentDirection = props.direction;
	}

}
