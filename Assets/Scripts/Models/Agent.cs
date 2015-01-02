using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : GameModel {

	public enum State {
		Idle,
		Wandering,
		Alert,
		Aggro,
		Controlled
	}

	public Room room { get; set; }
	public int Level { get; set; }
	public float Courage { get; set; }
	public float WanderSpeed { get; set; }
	public float AggroSpeed { get; set; }
	public State CurrentState { get; set; }

	public enum Direction {
		Up,
		Down,
		Right,
		Left,
		Stop
	}

	public float DetectRadius { get; set; }
	public float DetectTime { get; set; }
	public float AlertTime { get; set; }

	public float CurrentSpeed { get; set; }
	public Direction CurrentDirection { get; set; }
	public Vector3 Velocity { get { return CurrentSpeed * DirectionVector; } }

	public List<Vector3> waypoints {
		get {
			return room.waypoints;
		}
	}
	public int currentChaseIndex { get; set; }
	public Vector3 lastWaypoint { get; set; }

	public Vector3 DirectionVector {
		get {
			return Vector(CurrentDirection);
		}
	}

	public Agent.Direction NextDirection;
	public Vector3 NextVelocity {
		get {
			return Vector(NextDirection) * CurrentSpeed;
		}
	}

	public Vector3 Vector (Agent.Direction dir) {
		if (dir == Agent.Direction.Up) {
			return Vector3.forward;
		}
		if (dir == Agent.Direction.Right) {
			return Vector3.right;
		}
		if (dir == Agent.Direction.Left) {
			return Vector3.left;
		}
		if (dir == Agent.Direction.Down) {
			return Vector3.back;
		}
		if (dir == Agent.Direction.Stop) {
			return Vector3.zero;
		}
		
		return Vector3.zero;
	}

	public void SetDirectionVector (Vector3 dirV) {

		if (Mathf.Abs(dirV.normalized.x) > Mathf.Abs(dirV.normalized.z)) {
			if (dirV.x > 0f) {
				CurrentDirection = Agent.Direction.Right;
			} else {
				CurrentDirection = Agent.Direction.Left;
			}
		} else {
			if (dirV.z > 0f) {
				CurrentDirection = Agent.Direction.Up;
			} else {
				CurrentDirection = Agent.Direction.Down;
			}
		}
	}

	public void EnterState (Enemy.State newState) {
		CurrentState = newState;

		if (CurrentStateWandering) {
			CurrentSpeed = WanderSpeed;
		}

		if (CurrentStateAggro) {
			CurrentSpeed = AggroSpeed;
		}

		if (CurrentStateIdle || CurrentStateAlert) {
			CurrentSpeed = 0f;
		}
	}

	public bool CurrentStateIdle {
		get {
			return CurrentState == Enemy.State.Idle;
		}
	}

	public bool CurrentStateWandering {
		get {
			return CurrentState == Enemy.State.Wandering;
		}
	}

	public bool CurrentStateAlert {
		get {
			return CurrentState == Enemy.State.Alert;
		}
	}

	public bool CurrentStateAggro {
		get {
			return CurrentState == Enemy.State.Aggro;
		}
	}

	public bool CurrentStateControlled {
		get {
			return CurrentState == Enemy.State.Controlled;
		}
	}

	protected bool isOppositeDirection (Direction one, Direction other) {
		if (other == Direction.Up) {
			return (one == Direction.Down);
		}
		
		if (other == Direction.Right) {
			return (one == Direction.Left);
		}
		
		if (other == Direction.Down) {
			return (one == Direction.Up);
		}
		
		if (other == Direction.Left) {
			return (one == Direction.Right);
		}

		return false;
	}

	public void OnDetectPlayer (Player player) {
		currentChaseIndex = player.waypointsTraversed.Count - 1;
		EnterState(Agent.State.Alert);
	}

}
