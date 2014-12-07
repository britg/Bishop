using UnityEngine;
using System.Collections;

public class PlayerController : GameController {

	float consideredEqual = 0.001f;

	public Player player;
	public Player.Properties playerInitialization;

	Player.Direction nextDirection = Player.Direction.Up;
	bool nextWaypointSet = false;
	Vector3 nextWaypoint = Vector3.zero;

	void Awake () {
		player = new Player(playerInitialization);
	}

	void Update () {
		HandleDirectionChange();
		MovePlayer();
	}

	void MovePlayer () {
		ChooseNextWaypoint();
		transform.position += player.Velocity * Time.deltaTime;
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
			if (player.CurrentDirection != nextDirection) {
				Debug.Log ("Changing player direction at " + transform.position);
			}
			player.CurrentDirection = nextDirection;
			nextWaypoint = transform.position + player.DirectionVector;
		}
	}

	bool AtWaypoint () {
		return (Vector3.SqrMagnitude(transform.position - nextWaypoint) < consideredEqual);
	}
}
