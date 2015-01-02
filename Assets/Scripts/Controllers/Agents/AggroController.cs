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
			if (player.waypointsTraversed.Count > agent.currentChaseIndex+1) {
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
				agent.lastWaypoint = transform.position;
				agent.EnterState(Agent.State.Aggro);
			}
		} else if (agent.CurrentStateAggro) {
			Chase();
		}
	}

	void Chase () {
		var diff = currentWaypoint - agent.lastWaypoint;
		var nextPosition = transform.position + Time.deltaTime * agent.AggroSpeed * diff.normalized;

		Vector3 leftStart = transform.position;
		Vector3 leftDir = nextPosition;
		Vector3 rightStart = transform.position;
		Vector3 rightDir = nextPosition;
		if (diff.x > 0) {
			leftStart.z += 0.5f;
			leftDir.z += 0.5f;
			rightStart.z -= 0.5f;
			rightDir.z -= 0.5f;
		}

		if (diff.x < 0) {
			leftStart.z -= 0.5f;
			leftDir.z -= 0.5f;
			rightStart.z += 0.5f;
			rightDir.z += 0.5f;
		}

		if (diff.z > 0) {
			leftStart.x -= 0.5f;
			leftDir.x -= 0.5f;
			rightStart.x += 0.5f;
			rightDir.x += 0.5f;
		}

		if (diff.z < 0) {
			leftStart.x += 0.5f;
			leftDir.x += 0.5f;
			rightStart.x -= 0.5f;
			rightDir.x -= 0.5f;
		}

		RaycastHit[] centerHits = Physics.RaycastAll(transform.position, nextPosition - transform.position, 0.75f);
		RaycastHit[] leftHits = Physics.RaycastAll(leftStart, leftDir - leftStart, 0.75f);
		RaycastHit[] rightHits = Physics.RaycastAll(rightStart, rightDir - rightStart, 0.75f);

		Debug.DrawRay(transform.position, (nextPosition - transform.position).normalized, Color.white);
		Debug.DrawRay(leftStart, (leftDir - leftStart).normalized, Color.white);
		Debug.DrawRay(rightStart, (rightDir - rightStart).normalized, Color.white);

		List<RaycastHit> hits = new List<RaycastHit>();
		hits.AddRange (centerHits);
		hits.AddRange (leftHits);
		hits.AddRange (rightHits);

		foreach (RaycastHit hit in  hits) {
			if (BumpEnemy(hit)) {
				return;
			}
		}

		transform.position = nextPosition;
	}

	bool BumpEnemy (RaycastHit hit) {
		return (hit.collider.gameObject.tag == "Enemy" && hit.collider.gameObject.transform.parent != transform);
	}

}
