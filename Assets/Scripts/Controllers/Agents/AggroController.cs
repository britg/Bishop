using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AggroController : GameController {

	public Agent agent { get; set; }

	Player player;

	Vector3 up;
	Vector3 right;
	Vector3 down;
	Vector3 left;
	List<Vector3> availableDirs;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		availableDirs = new List<Vector3>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Paused) {
			return;
		}

		if (agent.CurrentStateAggro) {
			Chase();
		}
	}

	void Chase () {
		var nextWaypoint = NextWaypoint(transform.position);
		var direction = nextWaypoint - transform.position;
		var nextPosition = transform.position + Time.deltaTime * agent.CurrentSpeed * direction.normalized;
		transform.position = nextPosition;
	}

	Vector3 NextWaypoint (Vector3 currentWaypoint) {
		var playerPos = player.CurrentPosition;

		agent.waypoints.Sort ((a, b) => {
			return Vector3.Distance(a, currentWaypoint).CompareTo(Vector3.Distance(b, currentWaypoint));
		});

		List<Vector3> neighbors = new List<Vector3>(
			agent.waypoints.GetRange(0, 4)
				.Where(spot => Vector3.Distance(spot, currentWaypoint) <= 1.1f)
		);

		neighbors.Sort ((a, b) => {
			return Vector3.Distance(a, playerPos).CompareTo(Vector3.Distance(b, playerPos));
		});

		
		return neighbors[0];
	}

}
