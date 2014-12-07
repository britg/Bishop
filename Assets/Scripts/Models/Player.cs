using UnityEngine;
using System.Collections;

public class Player : GameModel {

	[System.Serializable]
	public class Properties {
		public float speed;
	}

	public enum Direction {
		Up,
		Down,
		Right,
		Left,
		Stop
	}

	public float Speed { get; set; }
	public Direction CurrentDirection { get; set; }

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

			return Vector3.forward;
		}
	}

	public Vector3 Velocity {
		get {
			return Speed * DirectionVector;
		}
	}

	public Player (Properties props) {
		Speed = props.speed;
		CurrentDirection = Direction.Stop;
	}

}
