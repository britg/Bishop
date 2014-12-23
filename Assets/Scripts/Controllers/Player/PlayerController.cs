using UnityEngine;
using System.Collections;

public class PlayerController : GameController {

	float consideredEqual = 0.001f;

	public Player player;
	public Player.Properties playerInitialization;
	public float playerAccel;

	public float collisionDistance = 0.5f;
	public float collisionRadius = 0.5f;

	float currentMoveDistance = 0f;
	float waypointDistance = 1f;

	bool nextWaypointInitialized = false;
	Vector3 nextWaypoint;
	Vector3 NextWaypoint {
		get {
			if (!nextWaypointInitialized) {
				nextWaypoint = transform.position;
			} 
			return nextWaypoint;
		}
		set {
			nextWaypointInitialized = true;
			nextWaypoint = value;
		}
	}

	bool canMove = false;

	PathingService pathingService;

	void Awake () {
		player = new Player(playerInitialization, gameObject);
		player.NextDirection = player.CurrentDirection;
		pathingService = new PathingService(player);
		Invoke ("EnableMovement", 1f);
	}

	void Update () {
		CheckDead();
		LockLane();

		if (ShouldTurn() && CanTurn()) {
			Turn();
		}

		if (player.CurrentDirection == Player.Direction.Stop) {
			return;
		}

		if (CanMove()) {
			MovePlayer();
			SpeedUp();
		} else {
			AttemptTurnOrStop();
//			StopPlayer();
		}
	}

	void MovePlayer () {
		Vector3 frameMove = player.Velocity * Time.deltaTime;
		transform.position += frameMove;
	}

	void AttemptTurnOrStop () {
		if (ShouldTurn()) {
			LockBoth();
			if (CanTurn()) {
				Turn();
				return;
			}
		}

		StopPlayer();
	}

	void StopPlayer () {
		player.CurrentDirection = Player.Direction.Stop;
		player.NextDirection = Player.Direction.Stop;
		LockBoth();
	}

	void SpeedUp () {
		player.CurrentSpeed += playerAccel * Time.deltaTime;
	}

	void SnapToNextWaypoint () {
		currentMoveDistance = waypointDistance;
		transform.position = NextWaypoint;
	}

	bool MovedPastWaypoint (float nextMoveDistance) {
		return nextMoveDistance > waypointDistance;
	}

	void LockLane () {
		if (player.CurrentDirection == Player.Direction.Stop) {
			LockBoth();
		}

		if (player.CurrentDirection == Player.Direction.Right ||
		    	player.CurrentDirection == Player.Direction.Left) {
			LockZ();
		}

		if (player.CurrentDirection == Player.Direction.Up ||
		    	player.CurrentDirection == Player.Direction.Down) {
			LockX();
		}
	}

	void LockBoth () {
		LockX();
		LockZ();
	}

	void LockX () {
		var pos = transform.position;
		pos.x = Mathf.Round(pos.x * 2f) / 2f;
		transform.position = pos;
	}

	void LockZ () {
		var pos = transform.position;
		pos.z = Mathf.Round(pos.z * 2f) / 2f;
		transform.position = pos;
	}

	bool CanMove() {
		if (!canMove) {
			return false;
		}
		var frameDestination = transform.position + (player.Velocity*Time.deltaTime);
		return pathingService.DestinationValid(frameDestination);
	}

	bool ShouldTurn () {
		return player.NextDirection != player.CurrentDirection;
	}

	bool CanTurn () {
		return pathingService.TurnValid(player.NextDirection);
	}

	void Turn () {
		player.CurrentDirection = player.NextDirection;
	}

	void EnableMovement () {
		canMove = true;
	}

	void DisableMovement () {
		canMove = false;
	}

	bool OnPivot () {
		var modX = transform.position.x % 1f < consideredEqual;
		var modZ = transform.position.z % 1f < consideredEqual;

		if (modX && modZ) {
//			Debug.Log ("Mod x is " + modX + " mod z is " + modZ);
			return true;
		}

		return false;
	}

	void CheckDead () {
		if (player.Dead) {
			DisableMovement();
		}
	}

}
