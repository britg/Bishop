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
	}
	
	// Update is called once per frame
	float currentAlertTime = 0f;
	void Update () {

		if (Paused) {
			return;
		}

		if (agent.CurrentStateAlert) {
			currentAlertTime += Time.deltaTime;
			if (currentAlertTime >= agent.AlertTime) {
				agent.EnterState(Agent.State.Aggro);
			}
		} else if (agent.CurrentStateAggro) {
			Chase();
		}
	}

	void Chase () {
		var diff = currentWaypoint - agent.lastWaypoint;
		transform.position += Time.deltaTime * agent.AggroSpeed * diff.normalized;
	}

}
