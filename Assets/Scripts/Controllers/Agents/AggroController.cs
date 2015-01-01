using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AggroController : GameController {

	public Agent agent { get; set; }

	Player player;
	List<Vector3> waypointsToWatch;

	Vector3 currentWaypoint {
		get {
			return player.waypointsTraversed[agent.currentChaseIndex];
		}
	}

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		SetWaypointsToWatch();
	}
	
	// Update is called once per frame
	void Update () {

		if (Paused) {
			return;
		}

		if (agent.CurrentState != Agent.State.Aggro) {
			WatchForPlayer();
		} else {
			Chase();
		}
	}

	void SetWaypointsToWatch () {
		waypointsToWatch = new List<Vector3>();
		waypointsToWatch.Add(transform.position + Vector3.right);
		waypointsToWatch.Add(transform.position + Vector3.right * 2f);
		waypointsToWatch.Add(transform.position + Vector3.up);
		waypointsToWatch.Add(transform.position + Vector3.down);
		waypointsToWatch.Add(transform.position + Vector3.left);
	}

	void WatchForPlayer () {
		foreach (var wp in waypointsToWatch) {
			if (player.lastWaypointTraversed.Equals(wp)) {
				agent.currentChaseIndex = player.waypointsTraversed.Count - 1;
				agent.EnterState(Agent.State.Aggro);
			}
		}
	}

	void Chase () {
		var dir = currentWaypoint - transform.position;
		transform.position += Time.deltaTime * dir.normalized * agent.CurrentSpeed;
	}

}
