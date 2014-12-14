using UnityEngine;
using System.Collections;

public class PlayerController : GameController {

	float consideredEqual = 0.001f;

	public Player player;
	public Player.Properties playerInitialization;

	Player.Direction nextDirection;
	bool nextWaypointSet = false;
	Vector3 nextWaypoint = Vector3.zero;

	bool canMove = false;

	void Awake () {
		player = new Player(playerInitialization);
		nextDirection = player.CurrentDirection;
		Invoke ("EnableMovement", 1f);
	}

	void Update () {
		CheckDead();
		HandleDirectionChange();
		if (canMove) {
			MovePlayer();
		}
	}

	void MovePlayer () {
		ChooseNextWaypoint();
		transform.position += player.Velocity * Time.deltaTime;
	}

	void EnableMovement () {
		canMove = true;
	}

	void DisableMovement () {
		canMove = false;
	}

	void HandleDirectionChange () {
		if (Input.GetKeyDown(KeyCode.W)) {
			nextDirection = Player.Direction.Up;
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			nextDirection = Player.Direction.Left;
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			nextDirection = Player.Direction.Down;
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			nextDirection = Player.Direction.Right;
		}
	}

	void ChooseNextWaypoint () {
		if (!nextWaypointSet) {
			nextWaypoint = transform.position + player.DirectionVector;
			nextWaypointSet = true;
		}

		if (AtWaypoint()) {
			player.CurrentDirection = nextDirection;
			nextWaypoint = transform.position + player.DirectionVector;

			if (!WaypointValid(nextWaypoint)) {
				player.CurrentDirection = Player.Direction.Stop;
			}
		}
	}

	bool AtWaypoint () {
		return player.CurrentDirection == Player.Direction.Stop ||
			(Vector3.SqrMagnitude(transform.position - nextWaypoint) < consideredEqual);
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
