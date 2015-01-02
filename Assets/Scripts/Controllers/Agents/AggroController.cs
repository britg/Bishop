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
			if (player.waypointsTraversed.Count > agent.currentChaseIndex) {
				return player.waypointsTraversed[agent.currentChaseIndex];
			} else {
				return player.lastWaypointTraversed;
			}
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
		waypointsToWatch.Add(transform.position + Vector3.forward);
		waypointsToWatch.Add(transform.position + Vector3.forward * 2f);
		waypointsToWatch.Add(transform.position + Vector3.back);
		waypointsToWatch.Add(transform.position + Vector3.back * 2f);
		waypointsToWatch.Add(transform.position + Vector3.left);
		waypointsToWatch.Add(transform.position + Vector3.left * 2f);
	}

	void WatchForPlayer () {
		foreach (var wp in waypointsToWatch) {
			if (player.lastWaypointTraversed.Equals(wp)) {
				agent.currentChaseIndex = player.waypointsTraversed.Count - 1;
				Invoke("EnterAggro", 1f);
			}
		}
	}

	void EnterAggro () {
		agent.EnterState(Agent.State.Aggro);
	}

	void Chase () {
		var diff = currentWaypoint - agent.lastWaypoint;
		transform.position += Time.deltaTime * agent.AggroSpeed * diff.normalized;
	}

}
