using UnityEngine;
using System.Collections;

public class PlayerController : GameController {

	float consideredEqual = 0.001f;

	public Player player;
	public Player.Properties playerInitialization;
	public float playerAccel;

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

	void Awake () {
		player = new Player(playerInitialization, gameObject);
		player.NextDirection = player.CurrentDirection;
		Invoke ("EnableMovement", 1f);
	}

	void Update () {
		CheckDead();
		if (canMove) {
			MovePlayer();
			player.CurrentSpeed += playerAccel * Time.deltaTime;
		}
	}

	void MovePlayer () {
		Vector3 frameMove = player.Velocity * Time.deltaTime;
		float frameMagnitude = player.CurrentSpeed * Time.deltaTime;
		float nextMoveDistance = currentMoveDistance + frameMagnitude;
		if (nextMoveDistance > waypointDistance) {
			currentMoveDistance = waypointDistance;
			transform.position = NextWaypoint;
		} else {
			currentMoveDistance += frameMagnitude;
			transform.position += frameMove;
		}

		if (AtWaypoint()) {
			currentMoveDistance = 0f;
			ChooseNextWaypoint();
		}
	}

	void EnableMovement () {
		canMove = true;
	}

	void DisableMovement () {
		canMove = false;
	}

	void ChooseNextWaypoint () {

		var inputNextWaypoint = transform.position + player.Vector(player.NextDirection);
		var continueNextWaypoint = transform.position + player.Vector(player.CurrentDirection);

		if (WaypointValid(inputNextWaypoint)) {
			player.CurrentDirection = player.NextDirection;
		} else if (WaypointValid(continueNextWaypoint)) {
			player.NextDirection = player.CurrentDirection;
		} else {
			player.CurrentDirection = Agent.Direction.Stop;
		}

		NextWaypoint = transform.position + player.DirectionVector;
	}

	bool AtWaypoint () {
		return player.CurrentDirection == Player.Direction.Stop ||
			(Vector3.SqrMagnitude(transform.position - nextWaypoint) < consideredEqual) ||
				currentMoveDistance >= waypointDistance;
	}

	bool WaypointValid (Vector3 waypoint) {
		float testDistance = 1.1f;
		float radius = 0.4f;
		var hits = Physics.SphereCastAll(transform.position, radius, waypoint - transform.position, testDistance);
		foreach (RaycastHit hit in hits) {
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Walls")) {
				return false;
			}
		}
		return true;
	}

	void CheckDead () {
		if (player.Dead) {
			DisableMovement();
		}
	}

}
